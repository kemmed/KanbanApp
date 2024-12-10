namespace KanbanApp.Models
{
    public enum UserRoles {  Watcher, Editor, Admin }
    public class UserBoard
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BoardID { get; set; }
        public UserRoles UserRole { get; set; }
        public User User { get; set; }
        public Board Board { get; set; }
        public static string RoleToString(UserRoles UR)
        {
            if (UR == UserRoles.Watcher)
                return "Наблюдатель";
            else if (UR == UserRoles.Editor)
                return "Редактор";
            else if (UR == UserRoles.Admin)
                return "Создатель";
            return "-";
        }
    }
    
}
