﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public string AdminLogin { get; set; }

        [Required]
        public string AdminPassword { get; set; }
    }
}
