using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.DTO
{
    public class MenuRolDTO
    {
        public int Id { get; set; }

        public int? IdMenu { get; set; }

        public int? IdRol { get; set; }

        public MenuDTO? IdMenuNavigation { get; set; }
        public RolDTO? IdRolNavigation { get; set; }
    }
}
