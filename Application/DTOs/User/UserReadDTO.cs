﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
    }
}
