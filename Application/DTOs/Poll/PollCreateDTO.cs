using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Poll
{
    public class PollCreateDTO
    {
        [Required(ErrorMessage = "The title is required.")]
        public string Title { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [Required(ErrorMessage = "You must provide at least one option.")]
        public List<string> Options { get; set; } = new List<string>();
    }
}
