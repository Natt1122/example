using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UserManage.BLL.service.contrato;
using UserManage.DAL.Repos.Contrato;
using UserManage.DTO;
using UserManage.Model;

namespace UserManage.BLL.service
{
    public class RolService :IRolService
    {
        private readonly IGenericRepository<Rol> _rolrepository;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolrepository, IMapper mapper)
        {
            _rolrepository = rolrepository;
            _mapper= mapper;
        }

        

        public async Task<List<RolDTO>> List()
        {
            try
            {
                var listaRols = await _rolrepository.Consult();
                return _mapper.Map<List<RolDTO>>(listaRols.ToList());
            }
            catch {
                throw;
            
            }
        }

        public async Task<RolDTO> Create(RolDTO modelo)
        {
            try
            {
                var createdRol = await _rolrepository.Create(_mapper.Map<Rol>(modelo));

                if (createdRol.Id == 0)
                    throw new TaskCanceledException("No se pudo crear");
                var query = await _rolrepository.Consult(r => r.Id == createdRol.Id);
                createdRol = query.First();

                return _mapper.Map<RolDTO>(createdRol);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Update(RolDTO modelo)
        {
            try
            {
                var modelRol = _mapper.Map<Rol>(modelo);
                var findRol = await _rolrepository.Get(r => r.Id == modelRol.Id);

                if (findRol == null)
                    throw new TaskCanceledException("El rol no existe");

                findRol.Name = modelRol.Name;

                bool answer = await _rolrepository.Update(findRol);

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
                var findRol = await _rolrepository.Get(r => r.Id == id);
                if (findRol == null)
                    throw new TaskCanceledException("El rol no existe");

                bool answer = await _rolrepository.Delete(findRol);
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
