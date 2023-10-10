﻿using System.ComponentModel.DataAnnotations;

namespace GraspItEz.Models
{
    public class QueryDto
    {
        [Required]
        [MaxLength(200)]
        public string Question { get; set; }
        [Required]
        [MaxLength(200)]
        public string Answer { get; set; }
        
    }
}
