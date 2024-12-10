namespace Chat.Application.Mappings;

public static class GroupMappings
{
    public static GroupDto MapToDto(this GroupChat groupChat)
    {
        return new GroupDto
        {
            Id = groupChat.Id,
            Name = groupChat.GroupName,
            CreatorId = groupChat.CreatorId
        };
    }

    public static GroupInfoDto MapToInfoDto(this GroupChat groupChat)
    {
        return new GroupInfoDto
        {
            Id = groupChat.Id,
            Name = groupChat.GroupName,
            MembersCount = groupChat.Members.Count
        };
    }

    public static GroupWithMembersDto MapToWithMembersDto(this GroupChat groupChat)
    {
        return new GroupWithMembersDto
        {
            Id = groupChat.Id,
            Name = groupChat.GroupName,
            CreatorId = groupChat.CreatorId,
            Members = groupChat.Members.Select(x => x.MapToDto()).ToList()
        };
    }
}