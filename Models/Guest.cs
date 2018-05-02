using System;
using System.Collections.Generic;

namespace wedding_planner.Models
{
    public class Guest
    {
        public int guestid { get; set; }
        public int userid { get; set; }
        public User user { get; set; }
        public int weddingid { get; set; }
        public Wedding wedding { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public Guest()
        {
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
        }
    }
}