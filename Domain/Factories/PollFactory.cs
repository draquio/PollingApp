using Domain.Aggregates;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factories
{
    public class PollFactory
    {
        public PollAggregate CreatePoll(string title, DateTime? expirationDate = null)
        {
            var poll = new Poll
            {
                Title = title,
                ExpirationDate = expirationDate
            };

            return new PollAggregate(poll);
        }
    }
}
