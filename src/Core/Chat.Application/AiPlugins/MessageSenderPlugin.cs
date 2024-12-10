namespace Chat.Application.AiPlugins;

public sealed class MessageSenderPlugin
{
    private readonly IMessageService _messageService;
    private readonly IUserService _userService;
    private readonly GroupChatService _groupChatService;

    public MessageSenderPlugin(
        IMessageService messageService,
        IUserService userService, GroupChatService groupChatService)
    {
        _messageService = messageService;
        _userService = userService;
        _groupChatService = groupChatService;
    }

    [KernelFunction]
    [Description("Sends a message to the chat")]
    [return: Description("The message that was sent")]
    public async Task<string> SendMessageAsync(
        Kernel kernel,
        [Description("The message for chat")] string message,
        [Description("The username of the user who sends the message. You can ask user's username if its not mentioned.")]
        string senderUsername,
        [Description("The title of the chat. If this title is not mentioned directly," +
                     " you can form it by using this formula: '{senderUsername} - {receiverUsername} Dialog'")]
        string chatTitle)
    {
        var senderId = (await _userService.GetUserByNameAsync(senderUsername)).Id;
        var groupChatId = (await _groupChatService.GetGroupChatByNameAsync(chatTitle)).Id;
        var messageDto = new MessageDto
        {
            Text = message,
            ConversationId = groupChatId,
            IsAiAssisted = true,
            SenderId = senderId
        };
        var messageResponse = await _messageService.CreateMessageAsync(messageDto);

        return messageResponse.Text;
    }
}