﻿namespace Chat.Domain.DTOs.Users;

public class PagedSearchDto
{
    public int Page { get; set; } = 1;
    public string? SearchFilter { get; set; } = string.Empty;
    public string SortingProperty { get; set; } = string.Empty;
    public SortingOrder SortingOrder { get; set; } = SortingOrder.Ascending;
}