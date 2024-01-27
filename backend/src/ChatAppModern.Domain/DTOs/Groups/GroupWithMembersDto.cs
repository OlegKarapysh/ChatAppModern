namespace ChatAppModern.Domain.DTOs.Groups;

public class GroupWithMembersDto : GroupDto
{
    public List<UserDto> Members { get; set; } = new();
}