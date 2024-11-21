﻿namespace UserManagementCommon.Models
{
    public class SearchRequestModel
    {
        public int pageSize { get; set; } = 10;
        public int pageNumber { get; set; }
        public string? searchText { get; set; }
    }
}
