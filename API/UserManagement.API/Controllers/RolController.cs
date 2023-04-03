using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManage.BLL.service.contrato;
using UserManage.DTO;
using UserManage.API.Utility;


namespace UserManage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }
        [HttpGet]
        [Route("RolList")]
        public async Task<IActionResult>List()
        {
            var rsp = new Response<List<RolDTO>>();
            try
            {
                rsp.status = true;
                rsp.Value = await _rolService.List();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg= ex.Message;
            }
            return Ok(rsp);
        }
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody] RolDTO rol)
        {
            var rsp = new Response<RolDTO>();

            try
            {
                rsp.status = true;
                rsp.Value = await _rolService.Create(rol);
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
        public async Task<IActionResult> Update([FromBody] RolDTO rol)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.Value = await _rolService.Update(rol);
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
                rsp.Value = await _rolService.Delete(id);
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
