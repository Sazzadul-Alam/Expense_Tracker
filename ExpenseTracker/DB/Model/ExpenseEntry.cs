using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.DB.Model
{
    public class ExpenseEntry
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("ExpenseCategory")]
        public int CatID { get; set; }
        public float Amount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [NotFutureDate(ErrorMessage = "Expenditure date cannot be a future date.")]
        public DateTime EntryDate { get; set; }
        public string Notes { get; set; }

        public virtual ExpenseCategory Category { set; get; }

    }
    public class NotFutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;
            return date <= DateTime.Now;
        }
    }
}
