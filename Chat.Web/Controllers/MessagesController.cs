using Chat.BusinessLogic.Services.Chats;
using Chat.BusinessLogic.Services.Messages;
using Chat.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers
{
    [Route("messages")]
    public class MessagesController: ApiControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MessageEntity message)
        {
            var result = await _messageService.Create(message);
            return Ok(new { id = result });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var messages = _messageService.GetAll();
            return Ok(new { messages });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var message = await _messageService.GetById(id);
            return Ok(new { message });
        }

        [HttpPut]
        public async Task<IActionResult> Update(MessageEntity message)
        {
            var result = await _messageService.Update(message);
            return Ok(new { result });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            await _messageService.Delete(id);
            return Ok();
        }
    }
}
