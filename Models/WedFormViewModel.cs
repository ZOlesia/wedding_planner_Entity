using System.ComponentModel.DataAnnotations;
using System;

namespace  wedding_planner.Models
{
    public class WedFormViewModel
    {
        [Display(Name = "Fiance")]
        [Required]
        public string wedder_one { get; set; }

        [Display(Name = "Fiancee")]
        [Required]
        public string wedder_two { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Wedding Date")]
        [Required]
        public DateTime date { get; set; }

        [Display(Name = "Wedding Address")]
        [Required]
        public string address { get; set; }
    }
}