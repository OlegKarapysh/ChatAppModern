namespace Chat.Application.Services.Messages;

public interface IMessageService
{
    Task<MessagesPageDto> SearchMessagesPagedAsync(PagedSearchDto searchData);
    Task<MessageWithSenderDto> CreateMessageAsync(MessageDto messageData);
    Task<List<MessageWithSenderDto>> GetAllPersonalChatMessagesAsync(int connectionId);
    Task<bool> DeleteMessageAsync(int messageId);
    Task<MessageDto> UpdateMessageAsync(MessageDto messageData, int updaterId);
}