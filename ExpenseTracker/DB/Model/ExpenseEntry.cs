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
        public DateTime EntryDate { get; set; }
        public string Notes { get; set; }

        public virtual ExpenseCategory Category { set; get; }

    }
}
