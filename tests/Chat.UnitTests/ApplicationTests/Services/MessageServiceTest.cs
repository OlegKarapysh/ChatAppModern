using Chat.Domain.Entities.Connections;
using MockQueryable;

namespace Chat.UnitTests.ApplicationTests.Services;

public sealed class MessageServiceTest
{
    private const int Id = 1;
    private const string UserName = "username";
    private const string MessageText = "this is message";
    private readonly IMessageService _sut;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<IRepository<PersonalMessage, int>> _messageRepositoryMock = new();

    public MessageServiceTest()
    {
        _unitOfWorkMock.Setup(x => x.GetRepository<PersonalMessage, int>()).Returns(_messageRepositoryMock.Object);
        _sut = new MessageService(_userServiceMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task SearchMessagesPagedAsync_ReturnsMessagesPage_WhenValidPagedSearchDto()
    {
        // Arrange.
        var (messages, expectedMessagesPage) = GetTestMessagesWithMessagesPage();
        var pageSearchDto = new PagedSearchDto
        {
            Page = expectedMessagesPage.PageInfo!.CurrentPage, SearchFilter = "filter",
            SortingProperty = nameof(PersonalMessage.Text), SortingOrder = SortingOrder.Descending
        };
        _messageRepositoryMock.Setup(x => x.SearchWhere<MessageBasicInfoDto>(pageSearchDto.SearchFilter))
                           .Returns(messages.AsQueryable());
        // Act.
        var result = await _sut.SearchMessagesPagedAsync(pageSearchDto);
        
        // Assert.
        using (new AssertionScope())
        {
            result.Should()!.BeOfType<MessagesPageDto>()!.And!.NotBeNull();
            result.Messages!.Should()!.NotBeNull()!.And!
                  .BeEquivalentTo(expectedMessagesPage.Messages!,
                      o => o.WithStrictOrdering()!
                            .Excluding(x => x.CreatedAt)!
                            .Excluding(x => x.UpdatedAt));
            result.PageInfo!.Should()!.NotBeNull()!.And!.BeEquivalentTo(expectedMessagesPage.PageInfo);
        }
    }

    [Fact]
    public async Task GetAllConversationMessagesAsync_ReturnsConversationMessages()
    {
        // Arrange.
        var conversationMessages = TestDataGenerator.GenerateMessagesForDialog(10, Id);
        var allMessages = TestDataGenerator.GenerateMessagesForDialog(10, Id + 1)
                                           .Concat(conversationMessages);
        var expectedMessages = conversationMessages.Select(x => x.MapToDtoWithSender());
        _messageRepositoryMock.Setup(x => x.AsQueryable()).Returns(allMessages.BuildMock()!);
        
        // Act.
        var result = await _sut.GetAllPersonalChatMessagesAsync(Id);

        // Assert.
        result.Should()!.BeEquivalentTo(expectedMessages);
    }
    
    [Fact]
    public async Task GetAllConversationMessagesAsync_ReturnsEmptyList_WhenNoMessagesInConversation()
    {
        // Arrange.
        var allMessages = TestDataGenerator.GenerateMessagesForDialog(10, Id + 1);
        _messageRepositoryMock.Setup(x => x.AsQueryable()).Returns(allMessages.BuildMock()!);
        
        // Act.
        var result = await _sut.GetAllPersonalChatMessagesAsync(Id);

        // Assert.
        result.Should()!.BeEmpty();
    }

    [Fact]
    public void UpdateMessageAsync_ThrowsEntityNotFoundException_WhenMessageNotFound()
    {
        // Arrange.
        _messageRepositoryMock.Setup(x => x.GetByIdAsync(Id)).ReturnsAsync((PersonalMessage)null!);
        
        // Act.
        var tryUpdateMessage = async () => await _sut.UpdateMessageAsync(new MessageDto(), Id);
        
        // Assert.
        tryUpdateMessage.Should()!.ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public void UpdateMessageAsync_ThrowsInvalidMessageUpdaterException_WhenInvalidUpdater()
    {
        // Arrange.
        const int invalidUpdaterId = Id + 1;
        var message = TestDataGenerator.GenerateMessage(Id);
        var messageDto = message.MapToDto();
        _messageRepositoryMock.Setup(x => x.GetByIdAsync(Id)).ReturnsAsync(message);

        // Act.
        var tryUpdateMessage = async () => await _sut.UpdateMessageAsync(messageDto, invalidUpdaterId);

        // Assert.
        tryUpdateMessage.Should()!.ThrowAsync<InvalidMessageUpdaterException>();
    }
    
    [Fact]
    public async Task UpdateMessageAsync_UpdatesMessage_WhenValidDto()
    {
        // Arrange.
        const string oldMessageText = "oldMessage";
        const string updatedMessageText = "newMessage";
        var message = TestDataGenerator.GenerateMessage(Id, oldMessageText);
        var updatedMessage = CopyMessage(message);
        updatedMessage.Text = updatedMessageText;
        var messageDto = updatedMessage.MapToDto();
        _messageRepositoryMock.Setup(x => x.GetByIdAsync(message.Id)).ReturnsAsync(message);
        _messageRepositoryMock.Setup(x => x.Update(
                                  It.Is<PersonalMessage>(m => m.Text == updatedMessageText && m.SenderId == message.SenderId)))
                              .Returns(updatedMessage);

        // Act.
        var result = await _sut.UpdateMessageAsync(messageDto, messageDto.SenderId);

        // Assert.
        using (new AssertionScope())
        {
            result.Should()!.BeEquivalentTo(messageDto);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }

    [Fact]
    public async Task DeleteMessageAsync_DeletesMessage_WhenMessageFound()
    {
        // Arrange.
        _messageRepositoryMock.Setup(x => x.RemoveAsync(Id)).ReturnsAsync(true);
        
        // Act.
        var result = await _sut.DeleteMessageAsync(Id);

        // Assert.
        using (new AssertionScope())
        {
            result.Should()!.BeTrue();
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }

    [Fact]
    public async Task CreateMessageAsync_CreatesMessageAndReturnsMessageDto_WhenValidDto()
    {
        // Arrange.
        const int conversationId = 5;
        const int senderId = 10;
        var messageDto = new MessageDto
        {
            Id = Id, ConversationId = conversationId, SenderId = senderId, Text = MessageText
        };
        var message = new PersonalMessage
        {
            Id = Id, ConnectionId = conversationId, SenderId = senderId, Text = MessageText
        };
        var sender = new User { UserName = UserName };
        _messageRepositoryMock.Setup(x => x.AddAsync(It.IsAny<PersonalMessage>()))
                               .ReturnsAsync(message);
        _userServiceMock.Setup(x => x.GetUserByIdAsync(senderId)).ReturnsAsync(sender);
        
        // Act.
        var result = await _sut.CreateMessageAsync(messageDto);

        // Assert.
        using (new AssertionScope())
        {
            result.UserName.Should()!.Be(sender.UserName);
            result.ConversationId.Should()!.Be(messageDto.ConversationId);
            result.SenderId.Should()!.Be(messageDto.SenderId);
            result.Text.Should()!.Be(messageDto.Text);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
    
    private (List<PersonalMessage> Messages, MessagesPageDto MessagesPage) GetTestMessagesWithMessagesPage()
    {
        return (new List<PersonalMessage>
        {
            new() { Text = "text01" },
            new() { Text = "text09" },
            new() { Text = "text02" },
            new() { Text = "text08" },
            new() { Text = "text03" },
            new() { Text = "text07" },
            new() { Text = "text04" },
            new() { Text = "text06" },
            new() { Text = "text05" },
            new() { Text = "text10" },
            new() { Text = "text11" },
        }, new MessagesPageDto
        {
            Messages = new MessageBasicInfoDto[]
            {
                new() { Text = "text06" },
                new() { Text = "text05" },
                new() { Text = "text04" },
                new() { Text = "text03" },
                new() { Text = "text02" },
            },
            PageInfo = new PageInfo { CurrentPage = 2, PageSize = PageInfo.DefaultPageSize, TotalCount = 11, TotalPages = 3 }
        });
    }

    private PersonalMessage CopyMessage(PersonalMessage message) => new()
    {
        Id = message.Id,
        ConnectionId = message.ConnectionId,
        SenderId = message.SenderId,
        Sender = message.Sender,
        Text = message.Text,
        CreatedAt = message.CreatedAt,
        UpdatedAt = message.UpdatedAt
    };
}