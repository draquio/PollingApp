using Domain.Entities;


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
    }
}
