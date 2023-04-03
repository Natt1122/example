﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string? Fullname { get; set; }

        public string? Email { get; set; }

        public int? IdRol { get; set; }

        public string? RolDescription { get; set; }

        public string? Password { get; set; }

        public int? IsActive { get; set; }
    }
}
