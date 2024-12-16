namespace KanbanApp.Models
{
    public class IssueColumn
    {
        public int ID { get; set; }
        public DateTime DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
        public int IssueID { get; set; }
        public Issue Issue { get; set; }

        public int ColumnID { get; set; }
        public Column Column { get; set; }
    }
}
