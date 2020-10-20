using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Core.DTOs.Account
{
    public class AssignRoleDto
    {
        public string Role { get; set; }
        public string UserId { get; set; }
    }
}
