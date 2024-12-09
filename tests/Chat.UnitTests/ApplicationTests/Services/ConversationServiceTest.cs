﻿namespace Chat.UnitTests.ApplicationTests.Services;

public sealed class ConversationServiceTest
{
    private const int Id = 1;
    private const string Title = "title1";
    private const string UserName = "username";
    private readonly IConversationService _sut;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<IRepository<ConversationParticipant, int>> _participantsRepositoryMock = new();
    private readonly Mock<IRepository<Conversation, int>> _conversationsRepositoryMock = new();

    public ConversationServiceTest()
    {
        _unitOfWorkMock.Setup(x => x.GetRepository<ConversationParticipant, int>())
                       .Returns(_participantsRepositoryMock.Object);
        _unitOfWorkMock.Setup(x => x.GetRepository<Conversation, int>())
                       .Returns(_conversationsRepositoryMock.Object);
        _sut = new ConversationService(_unitOfWorkMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task GetUserConversationIdsAsync_ReturnsAllConversationIds()
    {
        // Arrange.
        var conversationIds = new[] { 1, int.MaxValue };
        var conversationParticipants = new List<ConversationParticipant>
        {
            new() { ConversationId = conversationIds[0] }, new() { ConversationId = conversationIds[1] }
        };
        _participantsRepositoryMock.Setup(x =>
                                       x.FindAllAsync(It.IsAny<Expression<Func<ConversationParticipant, bool>>>()))
                                   .ReturnsAsync(conversationParticipants);
        // Act.
        var result = await _sut.GetUserConversationIdsAsync(default);

        // Assert.
        result.Should()!.BeEquivalentTo(conversationIds);
    }

    [Fact]
    public async Task SearchConversationsPagedAsync_ReturnsConversationsPage_WhenValidPageSearchDto()
    {
        // Arrange.
        var (conversations, expectedConversationsPage) = GetConversationsWithConversationsPage();
        var pageSearchDto = new PagedSearchDto
        {
            Page = expectedConversationsPage.PageInfo!.CurrentPage, SortingProperty = nameof(Conversation.Title),
            SortingOrder = SortingOrder.Descending, SearchFilter = "filter"
        };
        _conversationsRepositoryMock.Setup(x => x.SearchWhere<ConversationBasicInfoDto>(pageSearchDto.SearchFilter))
                                    .Returns(conversations.AsQueryable());
        // Act.
        var result = await _sut.SearchConversationsPagedAsync(pageSearchDto);
        
        // Assert.
        using (new AssertionScope())
        {
            result.Should()!.BeOfType<ConversationsPageDto>()!.And!.NotBeNull();
            result.Conversations!.Should()!.NotBeNull()!.And!.BeEquivalentTo(expectedConversationsPage.Conversations!,
                o => o.WithStrictOrdering()!.Excluding(x => x.CreatedAt)!.Excluding(x => x.UpdatedAt));
            result.PageInfo!.Should()!.NotBeNull()!.And!.BeEquivalentTo(expectedConversationsPage.PageInfo);
        }
    }

    [Fact]
    public async Task CreateOrGetGroupChatAsync_ReturnsGroup_WhenGroupAlreadyExists()
    {
        // Arrange.
        var newGroupDto = new NewGroupChatDto { CreatorId = Id, Title = Title };
        var creator = TestDataGenerator.GenerateUser();
        var existingGroup = new Conversation
        {
            Type = ConversationType.Group, Members = new List<User> { creator }, Title = Title, Id = Id
        };
        _userServiceMock.Setup(x => x.GetUserByIdAsync(newGroupDto.CreatorId)).ReturnsAsync(creator);
        _conversationsRepositoryMock.Setup(x => x.FindAllAsync(It.IsAny<Expression<Func<Conversation, bool>>>()))
                                    .ReturnsAsync(new List<Conversation> { existingGroup });
        // Act.
        var result = await _sut.CreateOrGetGroupChatAsync(newGroupDto);

        // Assert.
        using (new AssertionScope())
        {
            result.Should()!.NotBeNull();
            result.Id.Should()!.Be(existingGroup.Id);
            result.Title.Should()!.Be(existingGroup.Title);
            result.Type.Should()!.Be(existingGroup.Type);
        }
    }
    
    [Fact]
    public async Task CreateOrGetGroupChatAsync_ReturnsCreatedGroup_WhenGroupDoesntExist()
    {
        // Arrange.
        var newGroupDto = new NewGroupChatDto { CreatorId = Id, Title = Title };
        var creator = TestDataGenerator.GenerateUser();
        var createdGroupTitle = $"new {Title}";
        var createdGroup = new Conversation
        {
            Type = ConversationType.Group, Members = new List<User> { creator }, Title = createdGroupTitle, Id = Id
        };
        _userServiceMock.Setup(x => x.GetUserByIdAsync(newGroupDto.CreatorId)).ReturnsAsync(creator);
        _conversationsRepositoryMock.Setup(x => x.FindAllAsync(It.IsAny<Expression<Func<Conversation, bool>>>()))
                                    .ReturnsAsync(new List<Conversation>());
        _conversationsRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Conversation>()))
                                    .ReturnsAsync(createdGroup);
        // Act.
        var result = await _sut.CreateOrGetGroupChatAsync(newGroupDto);

        // Assert.
        using (new AssertionScope())
        {
            result.Should()!.NotBeNull();
            result.Id.Should()!.Be(createdGroup.Id);
            result.Title.Should()!.Be(createdGroup.Title);
            result.Type.Should()!.Be(createdGroup.Type);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
    
    [Fact]
    public async Task CreateOrGetDialogAsync_ReturnsDialog_WhenDialogAlreadyExists()
    {
        // Arrange.
        var creator = TestDataGenerator.GenerateUser();
        var interlocutor = TestDataGenerator.GenerateUser();
        var newDialogDto = new NewDialogDto { CreatorId = Id, InterlocutorUserName = interlocutor.UserName! };
        var existingDialog = new Conversation
        {
            Type = ConversationType.Dialog, Members = new List<User> { creator, interlocutor }, Title = Title, Id = Id
        };
        _userServiceMock.Setup(x => x.GetUserByIdAsync(newDialogDto.CreatorId)).ReturnsAsync(creator);
        _userServiceMock.Setup(x => x.GetUserByNameAsync(interlocutor.UserName!)).ReturnsAsync(interlocutor);
        _conversationsRepositoryMock.Setup(x => x.FindAllAsync(It.IsAny<Expression<Func<Conversation, bool>>>()))
                                    .ReturnsAsync(new List<Conversation> { existingDialog });
        // Act.
        var result = await _sut.CreateOrGetDialogAsync(newDialogDto);

        // Assert.
        using (new AssertionScope())
        {
            result.Should()!.NotBeNull();
            result.Id.Should()!.Be(existingDialog.Id);
            result.Title.Should()!.Be(existingDialog.Title);
            result.Type.Should()!.Be(existingDialog.Type);
        }
    }
    
    [Fact]
    public async Task CreateOrGetDialogAsync_ReturnsCreatedDialog_WhenDialogDoesntExist()
    {
        // Arrange.
        var creator = TestDataGenerator.GenerateUser();
        var interlocutor = TestDataGenerator.GenerateUser();
        var newDialogDto = new NewDialogDto { CreatorId = Id, InterlocutorUserName = interlocutor.UserName! };
        var createdDialog = new Conversation
        {
            Type = ConversationType.Dialog, Members = new List<User> { creator, interlocutor }, Title = Title, Id = Id
        };
        _userServiceMock.Setup(x => x.GetUserByIdAsync(newDialogDto.CreatorId)).ReturnsAsync(creator);
        _userServiceMock.Setup(x => x.GetUserByNameAsync(interlocutor.UserName!)).ReturnsAsync(interlocutor);
        _conversationsRepositoryMock.Setup(x => x.FindAllAsync(It.IsAny<Expression<Func<Conversation, bool>>>()))
                                    .ReturnsAsync(new List<Conversation>());
        _conversationsRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Conversation>()))
                                    .ReturnsAsync(createdDialog);
        // Act.
        var result = await _sut.CreateOrGetDialogAsync(newDialogDto);

        // Assert.
        using (new AssertionScope())
        {
            result.Should()!.NotBeNull();
            result.Id.Should()!.Be(createdDialog.Id);
            result.Title.Should()!.Be(createdDialog.Title);
            result.Type.Should()!.Be(createdDialog.Type);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }

    [Fact]
    public void RemoveUserFromConversationAsync_ThrowsEntityNotFoundException_WhenUserNotFound()
    {
        // Arrange.
        _participantsRepositoryMock
            .Setup(x => x.FindAllAsync(It.IsAny<Expression<Func<ConversationParticipant, bool>>>()))
            .ReturnsAsync(new List<ConversationParticipant>());
        // Act.
        var tryRemoveUserFromConversation = async () => await _sut.RemoveUserFromConversationAsync(Id, Id);
        
        // Assert.
        tryRemoveUserFromConversation.Should()!.ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task RemoveUserFromConversationAsync_RemovesUser_WhenUserFound()
    {
        // Arrange.
        const int conversationId = 3;
        var user = new ConversationParticipant { Id = Id, ConversationId = conversationId };
        _participantsRepositoryMock
            .Setup(x => x.FindAllAsync(It.IsAny<Expression<Func<ConversationParticipant, bool>>>()))
            .ReturnsAsync(new List<ConversationParticipant> { user });
        _participantsRepositoryMock.Setup(x => x.RemoveAsync(Id)).ReturnsAsync(true);
        
        // Act.
        var result = await _sut.RemoveUserFromConversationAsync(conversationId, Id);

        // Assert.
        using (new AssertionScope())
        {
            result.Should()!.BeTrue();
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }

    [Fact]
    public void GetConversationByIdAsync_ThrowsEntityNotFoundException_WhenInvalidId()
    {
        // Arrange.
        _conversationsRepositoryMock.Setup(x => x.GetByIdAsync(Id))
                                    .ReturnsAsync((Conversation)null!);
        // Act.
        var tryGetById = async () => await _sut.GetConversationByIdAsync(Id);
        
        // Assert.
        tryGetById.Should()!.ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public void AddGroupMemberAsync_ThrowsWrongConversationTypeException_WhenWrongConversationType()
    {
        // Arrange.
        var newMemberDto = new NewConversationMemberDto { ConversationId = Id };
        var conversation = new Conversation { Type = ConversationType.Dialog, Id = Id };
        _conversationsRepositoryMock.Setup(x => x.GetByIdAsync(newMemberDto.ConversationId))
                                    .ReturnsAsync(conversation);
        
        // Act.
        var tryAddGroupMember = async () => await _sut.AddGroupMemberAsync(newMemberDto);
        
        // Assert.
        tryAddGroupMember.Should()!.ThrowAsync<WrongConversationTypeException>();
    }
    
    [Fact]
    public async Task AddGroupMemberAsync_AddsNewGroupMemberAndReturnsGroupDto_WhenValidGroupMemberDto()
    {
        // Arrange.
        var newMemberDto = new NewConversationMemberDto { ConversationId = Id, MemberUserName = UserName };
        var member = new User { Id = Id, UserName = UserName };
        var conversation = new Conversation { Id = Id, Title = "title1", Type = ConversationType.Group };
        _conversationsRepositoryMock.Setup(x => x.GetByIdAsync(Id)).ReturnsAsync(conversation);
        _userServiceMock.Setup(x => x.GetUserByNameAsync(UserName)).ReturnsAsync(member);
        
        // Act.
        var result = await _sut.AddGroupMemberAsync(newMemberDto);

        // Assert.
        using (new AssertionScope())
        {
            result.Type.Should()!.Be(conversation.Type);
            result.Id.Should()!.Be(conversation.Id);
            result.Title.Should()!.Be(conversation.Title);
            _conversationsRepositoryMock.Verify(x => x.Update(conversation), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
    
    private (List<Conversation> Conversations, ConversationsPageDto ConversationsPage) GetConversationsWithConversationsPage() => new
    (
        new List<Conversation>
        {
            new() { Id = 1, Title = "title02", Type = ConversationType.Dialog },
            new() { Id = 2, Title = "title01", Type = ConversationType.Dialog },
            new() { Id = 3, Title = "title03", Type = ConversationType.Dialog },
            new() { Id = 4, Title = "title09", Type = ConversationType.Group },
            new() { Id = 5, Title = "title10", Type = ConversationType.Dialog },
            new() { Id = 6, Title = "title06", Type = ConversationType.Dialog },
            new() { Id = 7, Title = "title07", Type = ConversationType.Group },
            new() { Id = 8, Title = "title08", Type = ConversationType.Dialog },
            new() { Id = 9, Title = "title05", Type = ConversationType.Dialog },
            new() { Id = 8, Title = "title04", Type = ConversationType.Dialog },
            new() { Id = 9, Title = "title11", Type = ConversationType.Dialog },
        },
        new ConversationsPageDto
        {
            Conversations = new ConversationBasicInfoDto[]
            {
                new() { Title = "title06" },
                new() { Title = "title05" },
                new() { Title = "title04" },
                new() { Title = "title03" },
                new() { Title = "title02" },
            },
            PageInfo = new PageInfo { CurrentPage = 2, PageSize = PageInfo.DefaultPageSize, TotalCount = 11, TotalPages = 3 }
        }
    );
}