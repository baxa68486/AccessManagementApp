using System;
using System.Collections.Generic;

namespace DataAccessLayer.Pagination
{
    public class ResponseModel<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int PageNumber { get; private set; }
        public int TotalCount { get; private set; }
        public IEnumerable<T> Items { get; private set; }
        public string ArgumentsValidationErrorMessage { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                TotalPages = (int)Math.Ceiling((double)TotalCount / PageSize);
                return (CurrentPage < TotalPages);
            }
        }

        public ResponseModel(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber < 1 ? 1 : pageNumber;
            Items = items;
            PageNumber = pageNumber;
        }

        public ResponseModel(string errorMessage)
        {
            ArgumentsValidationErrorMessage = errorMessage;
        }
    }
}