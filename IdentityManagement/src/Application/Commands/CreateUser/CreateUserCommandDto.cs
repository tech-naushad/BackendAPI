﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Application.Commands.CreateUser
{
    public class CreateUserCommandDto
    {
        public string UserName { get; set; }      
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
