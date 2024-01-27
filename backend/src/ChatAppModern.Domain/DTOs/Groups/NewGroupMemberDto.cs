namespace ChatAppModern.Domain.DTOs.Groups;

public class NewGroupMemberDto
{
    public Guid GroupId { get; set; }
    public string MemberUserName { get; set; } = string.Empty;
}