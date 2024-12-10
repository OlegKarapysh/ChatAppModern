using Chat.Domain.Entities.Connections;

namespace Chat.Application.Mappings;

public static class MessageMappings
{
    private const string SqlDateTimeFormat = "yyyy-MM-dd HH:mm:ss.fffffff";
    
    public static MessageBasicInfoDto MapToBasicDto(this PersonalMessage message)
    {
        return new MessageBasicInfoDto
        {
            Text = message.Text,
            CreatedAt = message.CreatedAt.ToString(SqlDateTimeFormat, CultureInfo.InvariantCulture),
            UpdatedAt = message.UpdatedAt.ToString(SqlDateTimeFormat, CultureInfo.InvariantCulture)
        };
    }
    
    public static MessageDto MapToDto(this PersonalMessage message)
    {
        return new MessageDto
        {
            Id = message.Id,
            IsRead = message.IsRead,
            SenderId = message.SenderId ?? default,
            Text = message.Text,
            CreatedAt = message.CreatedAt.ToString(SqlDateTimeFormat, CultureInfo.InvariantCulture),
            UpdatedAt = message.UpdatedAt.ToString(SqlDateTimeFormat, CultureInfo.InvariantCulture)
        };
    }
    
    public static MessageWithSenderDto MapToDtoWithSender(this PersonalMessage message)
    {
        return new MessageWithSenderDto
        {
            Id = message.Id,
            IsRead = message.IsRead,
            SenderId = message.SenderId ?? default,
            UserName = message.Sender?.UserName ?? string.Empty,
            Text = message.Text,
            CreatedAt = message.CreatedAt.ToString(SqlDateTimeFormat, CultureInfo.InvariantCulture),
            UpdatedAt = message.UpdatedAt.ToString(SqlDateTimeFormat, CultureInfo.InvariantCulture)
        };
    }
    
    public static MessageWithSenderDto MapToDtoWithSender(this GroupMessage message)
    {
        return new MessageWithSenderDto
        {
            Id = message.Id,
            SenderId = message.SenderId ?? default,
            UserName = message.Sender?.UserName ?? string.Empty,
            Text = message.Text,
            CreatedAt = message.CreatedAt.ToString(SqlDateTimeFormat, CultureInfo.InvariantCulture),
            UpdatedAt = message.UpdatedAt.ToString(SqlDateTimeFormat, CultureInfo.InvariantCulture)
        };
    }
    
    public static PersonalMessage MapFrom(this PersonalMessage message, MessageDto messageDto)
    {
        message.Id = messageDto.Id;
        message.IsRead = messageDto.IsRead;
        message.SenderId = messageDto.SenderId;
        message.Text = messageDto.Text;
        
        return message;
    }

    public static MessageDto MapToDo(this MessageWithSenderDto message)
    {
        return new MessageDto
        {
            Id = message.Id,
            Text = message.Text,
            IsRead = message.IsRead,
            IsAiAssisted = message.IsAiAssisted,
            SenderId = message.SenderId,
            ConversationId = message.ConversationId,
            CreatedAt = message.CreatedAt,
            UpdatedAt = message.UpdatedAt
        };
    }
}