using MyExam.Models;
namespace MyExam.Agreement
{
    public class FilterSortModel
    {
        public List<FilterModel> filterModelList { get; set; }
        public SortModel sortModel { get; set; }
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
        public string sortColumn { get; set; } = "Status";
        public string sortType { get; set; } = "acsending";
    }
    public class PagingModel
    {
        public int start { get; set; } = 0;
        public int end { get; set; } = 50;
    }
    public class AgreementDTO
    {
        public List<AgreementModel> agreementList { get; set; }
        public int lastIndex { get; set; }
    }
}
