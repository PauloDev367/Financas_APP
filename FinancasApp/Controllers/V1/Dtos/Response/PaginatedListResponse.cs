using System;

namespace FinancasApp.Controllers.V1.Dtos.Response;
public class PaginatedListResponse<T>
{
    public List<T> Items { get; private set; }
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalCount { get; private set; } // Contagem total de itens

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public PaginatedListResponse(List<T> items, int pageIndex, int totalPages, int totalCount)
    {
        Items = items ?? new List<T>();
        PageIndex = pageIndex;
        TotalPages = totalPages;
        TotalCount = totalCount; // Adiciona o total de itens para referência
    }

    public static PaginatedListResponse<T> Create(List<T> items, int pageIndex, int pageSize, int totalCount)
    {
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        return new PaginatedListResponse<T>(items, pageIndex, totalPages, totalCount);
    }
}