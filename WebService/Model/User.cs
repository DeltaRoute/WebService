﻿using Microsoft.EntityFrameworkCore;

namespace WebService.Model
{
    [Keyless]
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
