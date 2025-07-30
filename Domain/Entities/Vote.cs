using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public int OptionId { get; set; }
        public string UserIp { get; set; }
        public DateTime VotedAt { get; set; } = DateTime.UtcNow;
        public Poll Poll { get; set; }
        public PollOption Option { get; set; }
    }
}
