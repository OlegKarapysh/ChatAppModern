namespace Chat.WebAPI.Controllers;

[ApiController, Authorize, Route("api/[controller]")]
public sealed class ConversationsController : ControllerBase
{
    private readonly IConnectionService _connectionService;

    public ConversationsController(IConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    // [HttpGet("search")]
    // public async Task<ActionResult<ConversationsPageDto>> SearchConversationsPagedAsync(
    //     [FromQuery] PagedSearchDto searchData)
    // {
    //     return Ok(await _connectionService.SearchConversationsPagedAsync(searchData));
    // }
    //
    // [HttpGet("all")]
    // public async Task<ActionResult<IList<ConversationDto>>> GetAllUserConversationAsync()
    // {
    //     return Ok(await _connectionService.GetAllUserConversationsAsync(HttpContext.User.GetIdClaim()));
    // }
    //
    // [HttpGet("all-ids")]
    // public async Task<ActionResult<IList<int>>> GetAllUserConversationIdsAsync()
    // {
    //     return Ok(await _connectionService.GetUserConnectionIdsAsync(HttpContext.User.GetIdClaim()));
    // }
    //
    // [HttpPost("dialogs")]
    // public async Task<ActionResult<DialogDto>> CreateDialogAsync(NewDialogDto newDialogData)
    // {
    //     if (newDialogData.CreatorId == default)
    //     {
    //         newDialogData.CreatorId = HttpContext.User.GetIdClaim();
    //     }
    //     
    //     return Ok(await _connectionService.GetOrCreateDialogAsync(newDialogData));
    // }
    //
    // [HttpPost("groups")]
    // public async Task<ActionResult<ConversationDto>> CreateGroupChatAsync(NewGroupChatDto newGroupChatData)
    // {
    //     if (newGroupChatData.CreatorId == default)
    //     {
    //         newGroupChatData.CreatorId = HttpContext.User.GetIdClaim();
    //     }
    //
    //     return Ok(await _connectionService.CreateOrGetGroupChatAsync(newGroupChatData));
    // }
    //
    // [HttpPost("members")]
    // public async Task<ActionResult<ConversationDto>> AddGroupMemberAsync(NewConversationMemberDto conversationMemberData)
    // {
    //     return Ok(await _connectionService.AddGroupMemberAsync(conversationMemberData));
    // }
    //
    // [HttpDelete("{conversationId:int}")]
    // public async Task<IActionResult> RemoveUserFromConversationAsync(int conversationId)
    // {
    //     var userId = HttpContext.User.GetIdClaim();
    //     var isSuccessfullyDeleted = await _connectionService.RemoveUserFromConversationAsync(conversationId, userId);
    //
    //     return isSuccessfullyDeleted ? NoContent() : NotFound();
    // }
}