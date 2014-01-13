﻿using System;
using System.Collections.Generic;

namespace iBank.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime PasswordExpireDate { get; set; }

        public virtual ICollection<Token> Token { get; set; }
    }
}