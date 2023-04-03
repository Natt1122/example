using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserManage.BLL.service.contrato;
using UserManage.DAL.Repos;
using UserManage.DAL.Repos.Contrato;
using UserManage.DTO;
using UserManage.Model;

namespace UserManage.BLL.service
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<User> _userrepository;
        private readonly IGenericRepository<MenuRol> _menurolrepository;
        private readonly IGenericRepository<Menu> _menurepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<User> userrepository,
            IGenericRepository<MenuRol> menurolrepository,
            IGenericRepository<Menu> menurepository, IMapper mapper)
        {
            _userrepository = userrepository;
            _menurolrepository = menurolrepository;
            _menurepository = menurepository;
            _mapper = mapper;
        }



        public async Task<List<MenuDTO>> MenuListById(int idUser)
        {
            IQueryable<User> tbuser = await _userrepository.Consult(u => u.Id == idUser);
            IQueryable<MenuRol> tbMenuRol = await _menurolrepository.Consult();
            IQueryable<Menu> tbMenu = await _menurepository.Consult();
            List<MenuDTO> listMenu = new List<MenuDTO>();
            try
            {
                IQueryable<Menu> tbanswer = (from u in tbuser
                                             join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                             join m in tbMenu on mr.IdMenu equals m.Id
                                             select m).AsQueryable();

                var listaMenus = tbanswer.ToList();
                return _mapper.Map<List<MenuDTO>>(listaMenus); ;
            }
            catch
            {
                throw;
            }
        }



        public async Task<bool> Update(MenuDTO modelo)
        {
            try
            {
                var menu = await _menurepository.Get(m => m.Id == modelo.Id);

                if (menu == null)
                {
                    throw new TaskCanceledException("El menú no existe");
                }
                menu.MenuRols.Clear();
                foreach (var roleId in modelo.RoleIds)
                {
                    var menuRol = new MenuRol { IdMenu = modelo.Id, IdRol = roleId };
                    menu.MenuRols.Add(menuRol);
                }

                await _menurepository.Update(menu);

                return true;
            }
            catch
            {
                throw;
            }
        }




        public async Task<List<MenuDTO>> GetMenus()
        {
            try
            {
                var menus = await _menurepository.Consult();
                return _mapper.Map<List<MenuDTO>>(menus.ToList());
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
