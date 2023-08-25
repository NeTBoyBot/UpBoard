using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Domain.Abtsractions;
using UpBoard.Domain.AdStates;
using UpBoard.Domain.Attributes;

namespace UpBoard.Domain
{
    public class Advertisement : Entity
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MinValue(0)]
        public decimal Price { get; set; }

        public AdStates.AdStates State { get; set; } = AdStates.AdStates.Moderated;

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public Guid OwnerId { get; set; }

        public User Owner { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
