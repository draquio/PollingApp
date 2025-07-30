using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Poll
{
    public class PollReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CreatedAt { get; set; }
        public string? ExpirationDate { get; set; }
        public List<PollOptionDTO> Options { get; set; }
    }
    public class PollOptionDTO
    {
        public int Id { get; set; }
        public string Option { get; set; }
    }
}
