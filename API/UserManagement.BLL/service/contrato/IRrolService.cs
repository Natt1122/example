using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManage.DTO;


namespace UserManage.BLL.service.contrato
{
    public interface IRolService
    {
        Task<List<RolDTO>> List();
        Task<RolDTO> Create(RolDTO modelo);
        Task<bool> Update(RolDTO modelo);
        Task<bool> Delete(int id);

    }
}
