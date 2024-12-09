﻿namespace Chat.WebAPI.Controllers;

[ApiController, Authorize, Route("api/[controller]")]
public sealed class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessagesController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet("search")]
    public async Task<ActionResult<MessagesPageDto>> SearchMessagesPagedAsync([FromQuery] PagedSearchDto searchData)
    {
        return Ok(await _messageService.SearchMessagesPagedAsync(searchData));
    }

    [HttpGet("all/{conversationId:int}")]
    public async Task<ActionResult<IList<MessageWithSenderDto>>> GetAllConversationMessagesAsync(int conversationId)
    {
        return Ok(await _messageService.GetAllConversationMessagesAsync(conversationId));
    }

    [HttpPost]
    public async Task<ActionResult<MessageWithSenderDto>> CreateMessageAsync(MessageDto messageData)
    {
        messageData.SenderId = HttpContext.User.GetIdClaim();
        return Ok(await _messageService.CreateMessageAsync(messageData));
    }

    [HttpPut]
    public async Task<ActionResult<MessageDto>> UpdateMessageAsync(MessageDto messageData)
    {
        var userId = HttpContext.User.GetIdClaim();
        return Ok(await _messageService.UpdateMessageAsync(messageData, userId));
    }
    
    [HttpDelete("{messageId:int}")]
    public async Task<IActionResult> DeleteMessageAsync(int messageId)
    {
        var isSuccessfullyDeleted = await _messageService.DeleteMessageAsync(messageId);
        return isSuccessfullyDeleted ? NoContent() : NotFound();
    }
}