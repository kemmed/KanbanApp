namespace KanbanApp.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string HashPass { get; set; }

        public List<UserBoard> UserBoards { get; set; }
    }
}
