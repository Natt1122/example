
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using UserManage.BLL.service.contrato;
using UserManage.DAL.Repos.Contrato;
using UserManage.DTO;
using UserManage.Model;

namespace UserManage.BLL.service
{
    public class UserService: IUserService
    {
        private readonly IGenericRepository<User> _userrepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userrepository, IMapper mapper)
        {
            _userrepository = userrepository;
            _mapper = mapper;
        }
        public async Task<List<UserDTO>> List()
        {
            try
            {
                var queryUser= await _userrepository.Consult();
                var listUser= queryUser.Include(rol=>rol.IdRolNavigation).ToList();

                return _mapper.Map<List<UserDTO>>(listUser);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SesionDTO> validateCredencials(string email, string password)
        {
            try
            {
                var queryUser = await _userrepository.Consult(u=>
                u.Email==email &&
                u.Password==password);
                if(queryUser.FirstOrDefault()==null)
                    throw new TaskCanceledException("El usuario No existe");

                User retuser=queryUser.Include(rol=>rol.IdRolNavigation).First();

                return _mapper.Map<SesionDTO>(retuser);
                    
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserDTO> Create(UserDTO modelo)
        {
            try
            {
                var createduser = await _userrepository.Create(_mapper.Map<User>(modelo));

                if (createduser.Id == 0)
                    throw new TaskCanceledException("No se pudo crear");
                var query = await _userrepository.Consult(u => u.Id == createduser.Id);
                createduser=query.Include(rol=> rol.IdRol).First();

                return _mapper.Map<UserDTO>(createduser);
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Update(UserDTO modelo)
        {
            try
            {
                var modeluser=_mapper.Map<User>(modelo);
                var finduser = await _userrepository.Get(u => u.Id == modeluser.Id);

                if(finduser==null)
                    throw new TaskCanceledException("El usuario no existe");

                finduser.Fullname = modeluser.Fullname;
                finduser.Email = modeluser.Email;
                finduser.IdRol= modeluser.IdRol;
                finduser.Password= modeluser.Password;
                finduser.IsActive= modeluser.IsActive;

                bool answer = await _userrepository.Update(finduser);

                if (!answer)
                    throw new TaskCanceledException("No se pudo editar");

                return answer;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var finduser = await _userrepository.Get(u => u.Id == id);
                if(finduser==null)
                    throw new TaskCanceledException("El usuario no existe");

                bool answer= await _userrepository.Delete(finduser);
                if (!answer)
                    throw new TaskCanceledException("No se pudo eliminar");

                return answer;
            }
            catch
            {
                throw;
            }
        }

      



       
    }
}
