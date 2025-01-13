using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VoteTracking
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public string UserIp { get; set; }
        public DateTime VotedAt { get; set; }
        public Poll Poll { get; set; }
    }
}
