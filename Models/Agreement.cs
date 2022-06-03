using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyExam.Models
{
    public class AgreementModel
    {
        [Key]
        public string? ID { get; set; }
        public string Status { get; set; }
        public string QuoteNumber { get; set; }
        public string AgreementName { get; set; }
        public string AgreementType { get; set; }
        public string DistributorName { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? DaysUntilExpiration { get; set; }
    }

}
