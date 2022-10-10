using Chat.BusinessLogic.Services.Chats;
using Chat.BusinessLogic.Services.Users;
using Chat.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    [Route("users")]
    public class UsersControllers : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UsersControllers(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserEntity user)
        {
            var result = await _userService.Create(user);
            return Ok(new { id = result });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            return Ok(new { result });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _userService.GetById(id);
            return Ok(new { result });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserEntity user)
        {
            var result = await _userService.Update(user);
            return Ok(new { result });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}
