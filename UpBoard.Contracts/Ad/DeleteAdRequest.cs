using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Contracts.Ad
{
    public class DeleteAdRequest
    {
        /// <summary>
        /// Id объявления
        /// </summary>
        public Guid Id { get; set; }

        
    }
}
