using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.FavoriteAd
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateFavoriteAdRequest
    {
        /// <summary>
        /// ID Объявления
        /// </summary>
        [Required]
        public Guid AdvertisementId { set; get; }
    }
}
