using Generic.Service.DTO.Abstract;

namespace Generic.Service.DTO.Concrete
{
    public enum PhraseType
    {
        Parentheses,
        Condition
    }
    public enum LogicalOperator
    {
        And,
        Or,
        Begin
    }
    public enum FilterOperator
    {
        Equals,
        NotEquals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public class GenericSearchRequestDto : AbstractGenericRequestDto
    {
        public List<GenericSearchFilterDto> filters { get; set; } = new List<GenericSearchFilterDto>();
        public List<GenericSearchSortDto> sorts { get; set; } = new List<GenericSearchSortDto>();
        public int? recordCount { get; set; } = null;
        public int? pageNumber { get; set; } = null;

        public override Task<bool> BeforeAction()
        {
            throw new NotImplementedException();
        }
    }
    public class GenericSearchResponseDto : AbstractGenericResponseDto
    {
        public override Task<bool> AfterAction()
        {
            throw new NotImplementedException();
        }
    }
    public class GenericSearchFilterDto
    {
        public string? columnName { get; set; }
        public string? value { get; set; }
        public PhraseType type { get; set; }
        public List<GenericSearchFilterDto>? InternalFilters { get; set; }
        public FilterOperator? operation { get; set; }
        public LogicalOperator LogicalOperator { get; set; }
    }
    public class GenericSearchSortDto
    {
        public string columnName { get; set; } // نام ستون
        public SortDirection direction { get; set; } // جهت مرتب‌سازی
    }
}
