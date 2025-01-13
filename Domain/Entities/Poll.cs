using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<PollOption> Options { get; set; } = new List<PollOption>();

        public bool IsExpired()
        {
            return ExpirationDate.HasValue && DateTime.UtcNow > ExpirationDate.Value;
        }
    }
}
