using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Domain.Abtsractions;

namespace UpBoard.Domain
{
    public class Category : Entity
    {
        /// <summary>
        /// Id родительской категории типа <see cref = "Category"/>
        /// </summary>
        public Guid? ParentId { get; set; }

        public Category? Parent { get; set; }

        /// <summary>
        /// Имя категории
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Список под категорий типа <see cref = "Category"/>
        /// </summary>
        public ICollection<Category> SubCategories { get; set; }

        /// <summary>
        /// Объявление под данной категорией
        /// </summary>
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
