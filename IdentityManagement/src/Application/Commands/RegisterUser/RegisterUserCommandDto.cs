using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Application.Commands.RegisterUser
{
    public class RegisterUserCommandDto
    {
        public string UserName { get; set; }      
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
