using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManage.DTO;

namespace UserManage.BLL.service.contrato
{
    public interface IUserService
    {
        Task<List<UserDTO>> List();
        Task<SesionDTO> validateCredencials(string email, string password);
        Task<UserDTO> Create(UserDTO modelo);
        Task<bool> Update(UserDTO modelo);
        Task<bool> Delete(int id);

    }
}
