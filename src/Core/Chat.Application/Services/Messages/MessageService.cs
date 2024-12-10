namespace Chat.Application.Services.Messages;

public sealed class MessageService : IMessageService
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<PersonalMessage, int> _personalMessageRepository;
    private readonly IRepository<GroupMessage, int> _groupMessageRepository;

    public MessageService(IUserService userService, IUnitOfWork unitOfWork)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
        _personalMessageRepository = _unitOfWork.GetRepository<PersonalMessage, int>();
        _groupMessageRepository = _unitOfWork.GetRepository<GroupMessage, int>();
    }

    public async Task<MessagesPageDto> SearchMessagesPagedAsync(PagedSearchDto searchData)
    {
        var foundMessages = _personalMessageRepository.SearchWhere<MessageBasicInfoDto>(searchData.SearchFilter);
        var messagesCount = await foundMessages.CountAsync();
        var pageSize = PageInfo.DefaultPageSize;
        var pageInfo = new PageInfo(messagesCount, searchData.Page);
        var foundMessagesPage = foundMessages
                                     .ToSortedPage(searchData.SortingProperty, searchData.SortingOrder, searchData.Page, pageSize)
                                     .Select(x => x.MapToBasicDto());

        return new MessagesPageDto
        {
            PageInfo = pageInfo,
            Messages = foundMessagesPage.ToArray()
        };
    }

    public async Task<List<MessageWithSenderDto>> GetAllPersonalChatMessagesAsync(int connectionId)
    {
        return await _personalMessageRepository.AsQueryable()
                                       .Include(x => x.Sender)
                                       .Where(x => x.ConnectionId == connectionId)
                                       .Select(x => x.MapToDtoWithSender())
                                       .ToListAsync();
    }
    
    public async Task<List<MessageWithSenderDto>> GetAllGroupChatMessagesAsync(int groupChatId)
    {
        return await _groupMessageRepository.AsQueryable()
                                       .Include(x => x.Sender)
                                       .Where(x => x.GroupChatId == groupChatId)
                                       .Select(x => x.MapToDtoWithSender())
                                       .ToListAsync();
    }

    public async Task<MessageWithSenderDto> CreateMessageAsync(MessageDto messageData)
    {
        var message = new PersonalMessage().MapFrom(messageData);
        var createdMessage = await _personalMessageRepository.AddAsync(message);
        await _unitOfWork.SaveChangesAsync();
        createdMessage.Sender = await _userService.GetUserByIdAsync(messageData.SenderId);

        return createdMessage.MapToDtoWithSender();
    }

    public async Task<MessageDto> UpdateMessageAsync(MessageDto messageData, int updaterId)
    {
        var message = await GetMessageByIdAsync(messageData.Id);
        if (message.SenderId != updaterId)
        {
            throw new InvalidMessageUpdaterException();
        }

        messageData.SenderId = updaterId;
        var updatedMessage = _personalMessageRepository.Update(message.MapFrom(messageData));
        await _unitOfWork.SaveChangesAsync();

        return updatedMessage.MapToDto();
    }

    public async Task<bool> DeleteMessageAsync(int messageId)
    {
        var isDeletedSuccessfully = await _personalMessageRepository.RemoveAsync(messageId);
        await _unitOfWork.SaveChangesAsync();

        return isDeletedSuccessfully;
    }

    public Task<PersonalMessage> GetMessageByIdAsync(int messageId)
    {
        var message = _personalMessageRepository.GetByIdAsync(messageId);
        if (message is null)
        {
            throw new EntityNotFoundException(nameof(PersonalMessage));
        }

        return message!;
    }
}