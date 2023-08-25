using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Contracts.Category
{
    public class UpdateCategoryRequest
    {

        /// <summary>
        /// ID Родительской категории
        /// </summary>
        [Required]
        public Guid? Id { get; set; }

        /// <summary>
        /// Имя категории
        /// </summary>
        [Required]
        [MinLength(3)]
        public string CategoryName { get; set; }

        /// <summary>
        /// ID Родительской категории
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
