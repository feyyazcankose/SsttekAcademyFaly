namespace Faly.Core;

public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; } // Sayfa verisi
    public int TotalCount { get; set; } // Toplam kayıt sayısı
    public int PageIndex { get; set; } // Mevcut sayfa
    public int PageSize { get; set; } // Sayfa başına düşen kayıt sayısı

    public PaginatedResult(IEnumerable<T> items, int totalCount, int pageIndex, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}
