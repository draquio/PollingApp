using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface INotificationService
    {
        Task NotifyVoteReceived(int pollId, int optionId, int votes);
    }
}
