using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManage.DTO;

namespace UserManage.BLL.service.contrato
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> MenuListById(int idUser);
        Task<bool> Update(MenuDTO modelo );
        Task<List<MenuDTO>> GetMenus();



    }
}
