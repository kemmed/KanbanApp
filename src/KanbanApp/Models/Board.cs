﻿namespace KanbanApp.Models
{
    public class Board
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int CreatorID { get; set; }
        public User CreatorUser { get; set; }

        public List<Column> Columns { get; set; }
        public List<UserBoard> UserBoards { get; set; }
        public static string StringCut(string str)
        {
            if (str.Length > 20)
            {
                str = str.Substring(0, 19) + "...";
            }
            return str;
        }
    }
}
