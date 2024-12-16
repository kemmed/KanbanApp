namespace KanbanApp.Models
{
    public enum IssueStatus { ToDo, InProcess, Review, Completed}
    public enum Priority {Low, Middle, High}
    public class Issue
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public IssueStatus Status { get; set; }
        public Priority Priority { get; set; }

        public int CreatorID { get; set; }
        public User Creator { get; set; }

        public int PerformerID { get; set; }
        public User Performer { get; set; }

        public List<IssueColumn> IssueColumns { get; set; }

        public static string StatusToString(IssueStatus IS)
        {
            if (IS == IssueStatus.ToDo)
                return "Сделать";
            else if (IS == IssueStatus.InProcess)
                return "В процессе";
            else if (IS == IssueStatus.Review)
                return "Проверяется";
            else if (IS == IssueStatus.Completed)
                return "Выполнена";
            return "-" ;
        }
        public static string PriorityToString(Priority IS)
        {
            if (IS == Priority.Low)
                return "низкий";
            else if (IS == Priority.Middle)
                return "средний";
            else if (IS == Priority.High)
                return "высокий";
            return "-";
        }

        public static string StringCut(string str)
        {
            if (str.Length > 16)
            {
                str = str.Substring(0, 15) + "...";  
            }
            return str;
        }

    }

   
}
