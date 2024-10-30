using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IquiriumBE.Domain.Enums
{
    public enum Roles
    {
        Administrator,
        Manager,
        User
    }
    public static class RoleHelper
    {
        public static string GetRoleName(Roles role)
        {
            return role.ToString();
        }
    }

}
