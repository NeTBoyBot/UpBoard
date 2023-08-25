using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Contracts.User
{
    public class DeleteUserRequest
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid Id { get; set; }
    }
}
