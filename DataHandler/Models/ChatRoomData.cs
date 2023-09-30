using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler.Models
{
    public class ChatRoomData
    {
        public Guid ChatRoomId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public List<Guid> Users { get; set; }
    }
}
