namespace Chat.Application.Services.Groups;

public class GroupChatService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<GroupChat, int> _groupRepository;

    public GroupChatService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _groupRepository = unitOfWork.GetRepository<GroupChat, int>();
    }

    public async Task<GroupChat?> GetGroupChatByNameAsync(string name)
    {
        var group = await _groupRepository.FindFirstAsync(g => g.GroupName == name);

        return group;
    }
}