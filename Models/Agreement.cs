using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyExam.Models
{
    public class Agreement
    {
        [Key]
        public string ID { get; set; }
        public string Status { get; set; }
        public string QuoteNumber { get; set; }
        public string AgreementName { get; set; }
        public string AgreementType { get; set; }
        public string DistributorName { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public string DaysUntilExpiration { get => ExpirationDate.Subtract(DateTime.Now.Date).Days.ToString(); }
    }

}
