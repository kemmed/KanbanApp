namespace KanbanApp.Models
{
    public class Issue
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string Status { get; set; }

        public int CreatorID { get; set; }
        public User Creator { get; set; }

        public int PerformerID { get; set; }
        public User Performer { get; set; }

        public int PriorityTypeID { get; set; }
        public PriorityType PriorityType { get; set; }

        public List<IssueColumn> IssueColumns { get; set; }

    }
}
