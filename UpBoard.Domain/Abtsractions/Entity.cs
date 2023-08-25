using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Domain.Abtsractions
{
    public abstract class Entity
    {
        /// <summary>
        /// Id сущности
        /// </summary>
        [Required]
        public Guid Id { get; set; }
    }
}
