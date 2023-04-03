using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManage.BLL.service.contrato;
using UserManage.DTO;
using UserManage.API.Utility;
using UserManage.BLL.service;
using Microsoft.EntityFrameworkCore;
using UserManage.Model;

namespace UserManage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("MenuListById")]
        public async Task<IActionResult> MenuListById(int idUser)
        {
            var rsp = new Response<List<MenuDTO>>();

            try
            {
                rsp.status = true;
                rsp.Value = await _menuService.MenuListById(idUser);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuDTO menuDTO)
        {
            if (id != menuDTO.Id)
            {
                return BadRequest();
            }
            try
            {
                bool result = await _menuService.Update(menuDTO);

                var rsp = new Response<bool>();
                rsp.status = true;
                rsp.Value = result;

                return Ok(rsp);
            }
            catch (Exception ex)
            {
                var rsp = new Response<bool>();
                rsp.status = false;
                rsp.msg = ex.Message;

                return Ok(rsp);
            }
        }



        [HttpGet]
        [Route("GetMenus")]
        public async Task<ActionResult>GetMenus()
        {
            var rsp = new Response<List<MenuDTO>>();
            try
            {
                rsp.status = true;
                rsp.Value = await _menuService.GetMenus();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
