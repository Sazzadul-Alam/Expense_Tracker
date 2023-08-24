using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DB.Model
{
    public class ExpenseCategory
    {
        [Key]
        public int ID { get; set; }
        public string CategoryName { get; set; }
    }
}
