using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Contracts.Comment
{
    public class UpdateCommentRequest
    {
        /// <summary>
        /// ID Комментария
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Содержимое отзыва
        /// </summary>
        [MinLength(1)]
        [Required]
        public string Text { get; set; }
    }
}
