using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public class PollAggregate
    {
        private readonly Poll _poll;

        public PollAggregate(Poll poll)
        {
            _poll = poll;
        }

        public Poll Poll => _poll;
        public void AddOption(string optionText)
        {
            if (_poll.Options.Count >= 10) throw new InvalidOperationException("No se permiten más de 10 opciones.");
            _poll.Options.Add(new PollOption { OptionText = optionText });
        }

        public void RegisterVote(int optionId)
        {
            var option = _poll.Options.FirstOrDefault(o => o.Id == optionId);
            if (option == null) throw new InvalidOperationException("Opción no encontrada.");
            option.AddVote();
        }
    }
}
