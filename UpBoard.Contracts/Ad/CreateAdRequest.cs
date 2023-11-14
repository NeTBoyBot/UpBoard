using Board.Contracts.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Ad
{
    /// <summary>
    /// Создание объявления
    /// </summary>
    public class CreateAdvertisementRequest
    {

        ///// <summary>
        ///// Id владельца объявления
        ///// </summary>
        //public Guid OwnerId { get; set; }

        /// <summary>
        /// Имя объявления
        /// </summary>
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        /// <summary>
        /// Описание объявления
        /// </summary>
        [MinLength(10)]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Стоимость объявления
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// ID Категории
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }

        
       
    }
}
