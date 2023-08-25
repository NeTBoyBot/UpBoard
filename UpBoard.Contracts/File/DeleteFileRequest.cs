using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Contracts.File
{
    public class DeleteFileRequest
    {
        /// <summary>
        /// Id файла
        /// </summary>
        public Guid Id { get; set; }
    }
}
