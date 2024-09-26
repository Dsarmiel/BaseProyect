namespace Cotizaciones.Utils.Filters
{
    public class ProductsFilters
    {
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public Decimal? InitialPrice { get; set; } = null;
        public Decimal? FinalPrice { get; set; } = null;
        public Guid? CategoryId { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
