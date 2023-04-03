using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.DTO
{
    public class MenuDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Url { get; set; }

        public List<int>? RoleIds { get; set; }
    }
}
