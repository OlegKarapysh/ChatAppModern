namespace Chat.Application.Services.Connections;

public interface IConnectionService
{
    Task<List<int>> GetUserConnectionIdsAsync(int userId);
}