using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Validation
{
    public class ValidationService : IValidationService
    {
        public void IsValidId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID.");
            }
        }
    }
}
