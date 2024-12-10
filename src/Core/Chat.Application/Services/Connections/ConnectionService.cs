namespace Chat.Application.Services.Connections;

public sealed class ConnectionService : IConnectionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IRepository<Connection, int> _connectionRepository;

    public ConnectionService(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _connectionRepository = _unitOfWork.GetRepository<Connection, int>();
    }

    public async Task<List<int>> GetUserConnectionIdsAsync(int userId)
    {
        return (await _connectionRepository
            .FindAllAsync(x => x.InitiatorId == userId || x.InviteeId == userId))
            .Select(x => x.Id)
            .ToList();
    }
}