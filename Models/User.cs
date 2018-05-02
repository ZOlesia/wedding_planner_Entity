using System;
using System.Collections.Generic;


namespace wedding_planner.Models
{
    public class User
    {

        public int userid { get; set; }
        public string first_name { get; set; }

        public string last_name { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<Wedding> weddings { get; set; }
        public List<Guest> visitants { get; set; }


        public User()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            weddings = new List<Wedding>();
            visitants = new List<Guest>();
        }
    }
}