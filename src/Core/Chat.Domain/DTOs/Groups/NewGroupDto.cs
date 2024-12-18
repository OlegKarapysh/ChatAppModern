﻿namespace Chat.Domain.DTOs.Groups;

public class NewGroupDto
{
    [Required(ErrorMessage = "Group name is required!")]
    [MaxLength(GroupChat.MaxNameLength,
        ErrorMessage = "Group name must be no more than 100 characters long")]
    public string Name { get; set; } = string.Empty;
    [MaxLength(GroupChat.MaxInstructionsLength,
        ErrorMessage = "Instructions must be no more than 1000 characters long")]
    public string Instructions { get; set; } = string.Empty;
    public int? CreatorId { get; set; }
}