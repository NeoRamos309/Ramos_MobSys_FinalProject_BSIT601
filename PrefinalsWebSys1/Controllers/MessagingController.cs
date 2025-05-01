using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrefinalsWebSys1.Models;

namespace PrefinalsWebSys1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        [HttpPost]
        public MessageViewModel SendMessage([FromForm]MessageViewModel req)
        {
            var resp = new MessageViewModel();

            using (var db = new SmileBookDBContext())
            {
                var newMessage = new UserMessage()
                {
                    FromUserID = req.SenderUserID,
                    MessageBody = req.SenderMessage,
                    MessageType = "NORMAL",
                    Priorty = 0,
                    SentDate = DateTime.Now,
                    ReceivedDate = DateTime.Now,
                    ToUserID = req.ReceiverUserID,
                    IsDeleted = false,
                    CreatedBy = "SYSTEM",
                    ModifiedBy = "SYSTEM",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };

                db.UserMessages.Add(newMessage);
                db.SaveChanges();

                resp.SenderUserID = req.SenderUserID;
                resp.MessageID = newMessage.ID;
                resp.Status = "SENT";

            }

            return resp;
        }

        [HttpGet]
        public List<UserMessage> GetMessages(int UserID)
        {
            var resp = new List<UserMessage>();

            using(var db = new SmileBookDBContext())
            {
                resp = (from rw in db.UserMessages 
                        where 
                        (rw.FromUserID == UserID
                        || rw.ToUserID == UserID)
                        && !rw.IsDeleted
                        select rw).ToList();
            }

            return resp;
        }
    }
}
