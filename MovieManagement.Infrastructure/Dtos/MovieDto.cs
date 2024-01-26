using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace MovieManagement.Infrastructure.Dtos
{
    public class MovieDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public decimal TicketPrice { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public List<string> Genres { get; set; }
        [Required]
        public string Photo { get; set; }
    }
}
