﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Contracts.FavoriteAd
{
    public class DeleteFavoriteAdRequest
    {
        /// <summary>
        /// Id объявления
        /// </summary>
        public Guid Id { get; set; }
    }
}
