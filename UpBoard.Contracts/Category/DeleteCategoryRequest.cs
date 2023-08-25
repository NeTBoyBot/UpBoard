using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Contracts.Category
{
    public class DeleteCategoryRequest
    {
        /// <summary>
        /// Id категории
        /// </summary>
        public Guid Id { get; set; }
    }
}
