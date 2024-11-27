namespace KanbanApp.Models
{
    public class PriorityType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int BoardID { get; set; }
        public Board Board { get; set; }

        public List<Issue> Issues { get; set; }
    }
}
