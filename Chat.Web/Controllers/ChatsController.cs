using Chat.BusinessLogic.Services.Chats;
using Chat.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers;

[Route("chats")]
public class ChatsController : ApiControllerBase
{
    private readonly IChatService _chatService;

    public ChatsController(IChatService chatService)
    {
        _chatService = chatService;
    }

    // chat.com/chats
    [HttpPost]
    public async Task<IActionResult> Create(ChatEntity chat)
    {
        var result = await _chatService.Create(chat);
        
        // todo: 201 status code
        return Ok(new {id = result});
    }
    
    //chat.com/chats
    [HttpGet]
    public IActionResult GetAll()
    {
        var chats = _chatService.GetAll();
        return Ok(new {chats});
    }

    //chat.com/chats/123
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var chat = await _chatService.GetById(id);  
        return Ok(new {chat});
    }

    [HttpPut]
    public async Task<IActionResult> Update(ChatEntity chat)
    {
        var result = await _chatService.Update(chat);
        return Ok(new {result});
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(long id)
    {
        await _chatService.Delete(id);
        return Ok();
    }
}