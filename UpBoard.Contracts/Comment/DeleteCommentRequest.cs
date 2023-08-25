using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Contracts.Comment
{
    public class DeleteCommentRequest
    {
        /// <summary>
        /// Id комментария
        /// </summary>
        public Guid Id { get; set; }
    }
}
