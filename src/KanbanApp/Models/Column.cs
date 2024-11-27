namespace KanbanApp.Models
{
    public class Column
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int BoardID { get; set; }
        public Board Board { get; set; }

        public List<IssueColumn> IssueColumns { get; set; }
    }
}
