﻿namespace Chat.Application.Services.Conversations;

public sealed class ConversationService : IConversationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IRepository<ConversationParticipant, int> _participantsRepository;
    private readonly IRepository<Conversation, int> _conversationsRepository;

    public ConversationService(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _participantsRepository = _unitOfWork.GetRepository<ConversationParticipant, int>();
        _conversationsRepository = _unitOfWork.GetRepository<Conversation, int>();
    }

    public async Task<IList<int>> GetUserConversationIdsAsync(int userId)
    {
        return (await _participantsRepository
            .FindAllAsync(x => x.UserId == userId))
            .Select(x => x.ConversationId ?? default)
            .ToArray();
    }

    public async Task<ConversationDto> AddGroupMemberAsync(NewConversationMemberDto conversationMemberData)
    {
        var conversation = await GetConversationByIdAsync(conversationMemberData.ConversationId);
        if (conversation.Type != ConversationType.Group)
        {
            throw new WrongConversationTypeException();
        }
        
        var newMember = await _userService.GetUserByNameAsync(conversationMemberData.MemberUserName);
        conversation.Members.Add(newMember);
        _conversationsRepository.Update(conversation);
        await _unitOfWork.SaveChangesAsync();

        return conversation.MapToDto();
    }

    public async Task<ConversationDto> CreateOrGetGroupChatAsync(NewGroupChatDto newGroupChatData)
    {
        var creator = await _userService.GetUserByIdAsync(newGroupChatData.CreatorId);
        var existingGroupChat = (await _conversationsRepository.FindAllAsync(conversation =>
                conversation.Type == ConversationType.Group &&
                conversation.Members.Contains(creator) &&
                conversation.Title == newGroupChatData.Title))
                .FirstOrDefault();
        if (existingGroupChat is not null)
        {
            return existingGroupChat.MapToDto();
        }

        var groupChat = new Conversation
        {
            Type = ConversationType.Group,
            Title = newGroupChatData.Title,
            Members = new List<User> { creator }
        };
        var createdGroupChat = await _conversationsRepository.AddAsync(groupChat);
        await _unitOfWork.SaveChangesAsync();
        
        return createdGroupChat.MapToDto();
    }

    public async Task<DialogDto> CreateOrGetDialogAsync(NewDialogDto newDialogData)
    {
        var interlocutor = await _userService.GetUserByNameAsync(newDialogData.InterlocutorUserName);
        var creator = await _userService.GetUserByIdAsync(newDialogData.CreatorId);
        var existingDialog = (await _conversationsRepository.FindAllAsync(conversation =>
            conversation.Type == ConversationType.Dialog &&
            conversation.Members.Contains(creator) &&
            conversation.Members.Contains(interlocutor)))
            .FirstOrDefault();
        if (existingDialog is not null)
        {
            return existingDialog.MapToDialogDto();
        }

        var dialog = new Conversation
        {
            Type = ConversationType.Dialog,
            Title = $"{creator.UserName} - {interlocutor.UserName} Dialog",
            Members = new List<User> { creator, interlocutor }
        };
        var createdDialog = await _conversationsRepository.AddAsync(dialog);
        await _unitOfWork.SaveChangesAsync();
        
        return createdDialog.MapToDialogDto();
    }

    public async Task<IList<ConversationDto>> GetAllUserConversationsAsync(int userId)
    {
        var userConversationIds = await GetUserConversationIdsAsync(userId);
        return (await _conversationsRepository.FindAllAsync(x => userConversationIds.Contains(x.Id)))
               .Select(x => x.MapToDto())
               .ToArray();
    }

    public async Task<ConversationsPageDto> SearchConversationsPagedAsync(PagedSearchDto searchData)
    {
        var foundConversations = _conversationsRepository.SearchWhere<ConversationBasicInfoDto>(searchData.SearchFilter);
        var conversationsCount = foundConversations.Count();
        var pageSize = PageInfo.DefaultPageSize;
        var pageInfo = new PageInfo(conversationsCount, searchData.Page);
        var foundConversationsPage = foundConversations
                                     .ToSortedPage(searchData.SortingProperty, searchData.SortingOrder, searchData.Page, pageSize)
                                     .Select(x => x.MapToBasicDto());
        
        return await Task.FromResult(new ConversationsPageDto
        {
            PageInfo = pageInfo,
            Conversations = foundConversationsPage.ToArray()
        });
    }

    public async Task<bool> RemoveUserFromConversationAsync(int conversationId, int userId)
    {
        var conversationMember = (await _participantsRepository
                .FindAllAsync(x => x.ConversationId == conversationId && x.UserId == userId))
                .FirstOrDefault();
        if (conversationMember is null)
        {
            throw new EntityNotFoundException("Conversation member");
        }

        var isSuccessfullyRemoved = await _participantsRepository.RemoveAsync(conversationMember.Id);
        await _unitOfWork.SaveChangesAsync();

        return isSuccessfullyRemoved;
    }

    public async Task<Conversation> GetConversationByIdAsync(int id)
    {
        return await _conversationsRepository.GetByIdAsync(id) ??
            throw new EntityNotFoundException(nameof(Conversation));
    }

    public async Task<ConversationDto> GetConversationByTitleAsync(string conversationTitle)
    {
        var conversation = await _conversationsRepository.FindFirstAsync(x => x.Title == conversationTitle);
        if (conversation is null)
        {
            throw new EntityNotFoundException(nameof(Conversation));
        }
        
        return conversation.MapToDto();
    }
}