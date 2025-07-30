using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PollOption
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public int PollId { get; set; }
        public Poll Poll { get; set; }
    }
}
