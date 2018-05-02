using System;
using System.Collections.Generic;

namespace wedding_planner.Models
{
    public class Wedding
    {
        public int weddingid { get; set; }
        public string wedder_one { get; set; }
        public string wedder_two { get; set; }
        public DateTime date { get; set; }
        public string address { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; } 
        public int userid { get; set; }
        public User user { get; set; }
        public List<Guest> guests { get; set; }

        public Wedding()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            guests = new List<Guest>();
        }
    }
}