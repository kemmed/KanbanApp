﻿namespace KanbanApp.Models
{
    public enum UserRoles { Admin, Editor, Watcher}
    public class UserBoard
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BoardID { get; set; }
        public UserRoles UserRole { get; set; }
        public User User { get; set; }
        public Board Board { get; set; }
    }
}
