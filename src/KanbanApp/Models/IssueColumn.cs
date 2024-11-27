namespace KanbanApp.Models
{
    public class IssueColumn
    {
        public int ID { get; set; }
        public DateTime AssignDate { get; set; }

        public int IssueID { get; set; }
        public Issue Issue { get; set; }

        public int ColumnID { get; set; }
        public Column Column { get; set; }
    }
}
