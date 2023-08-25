using System.ComponentModel.DataAnnotations;
using UpBoard.Domain.Abtsractions;
using UpBoard.Domain.UserStates;

namespace UpBoard.Domain
{
    /// <summary>
    /// Класс Пользователя
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Дата регистрации пользователя
        /// </summary>
        public DateTime Registrationdate { get; set; }

        /// <summary>
        /// Является ли аккаунт верифицированным
        /// </summary>
        public UserStates.UserStates UserState { get; set; }

        /// <summary>
        /// Рейтинг пользователя
        /// </summary>
        public double Rating { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }
        public ICollection<FavoriteAd> FavoriteAds { get; set; }
        public ICollection<Comment> RecievedComments { get; set; }
        public ICollection<Comment> SendedComments { get; set; }


    }
}
