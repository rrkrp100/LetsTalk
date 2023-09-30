using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LetsTalk.Models;
using LetsTalk.Services;

namespace LetsTalk.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ChatController : ControllerBase
    {
        IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        /// <summary>
        /// Get msgs in the given page
        /// </summary>
        [HttpGet("{chatRoomId}/{page}")]
        public async Task<ActionResult<ConversationViewModel>> Get(Guid chatRoomId, int page=-1)
        {
            var res = await _chatService.GetMessages(chatRoomId, page);
            return Ok(res);
        }

        // POST api/<ChatRoomController>
        [HttpPost]
        public async Task<ActionResult<MessageViewModel>> SendMessage([FromBody] MessageModel sendMessage)
        {
            try
            {
                var res = await _chatService.SendMessage(sendMessage);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ChatRoomController>/5
        [HttpDelete("{msgId}")]
        public void Delete(int id)
        {
        }

        /// <summary>
        /// Following are chat Room Operations
        /// </summary>

        //Create new Chat Room
        [HttpPost("chatroom")]
        public void CreateChatRoom()
        {
        }

        //Add user to chatroom
        [HttpPut("chatroom/{userId}")]
        public void AddUser(Guid userId)
        {

        }

        //Delete Chat Room
        [HttpDelete("chatroom/{ChatRoomId}")]
        public void DeleteChatRoom(Guid chatRoomId)
        {
        }
    }
}
