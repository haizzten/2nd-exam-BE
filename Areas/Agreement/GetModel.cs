namespace MyExam.Agreement
{
    public class FilterSortModel
    {
        FilterModel[] filterModelList { get; set; }
        SortModel sortModel { get; set; }
    }
    public class FilterModel
    {
        public string columnName { get; set; }
        public string valueType { get; set; }
        public string filterType { get; set; }
        public string filterValue { get; set; }
    }
    public class SortModel
    {
        public string sortColumn { get; set; }
        public string sortType { get; set; }
    }
    public class PagingModel
    {
        public int start { get; set; } = 0;
        public int end { get; set; } = 50;
    }
}
