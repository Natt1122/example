using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManage.BLL.service.contrato;
using UserManage.DTO;
using UserManage.API.Utility;
using UserManage.BLL.service;


namespace UserManage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("ListUser")]
        public async Task<IActionResult> List()
        {
            var rsp = new Response<List<UserDTO>>();

            try
            {
                rsp.status = true;
                rsp.Value = await _userService.List();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] loginDTO login)
        {
            var rsp = new Response<SesionDTO>();

            try
            {
                rsp.status = true;
                rsp.Value = await _userService.validateCredencials(login.Email, login.Password);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody] UserDTO user)
        {
            var rsp = new Response<UserDTO>();

            try
            {
                rsp.status = true;
                rsp.Value = await _userService.Create(user);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UserDTO user)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.Value = await _userService.Update(user);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.Value = await _userService.Delete(id);
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
