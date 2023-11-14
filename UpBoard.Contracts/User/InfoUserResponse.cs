using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.User
{
    public class InfoUserResponse
    {
        /// <summary>
        /// ID Пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Дата создания аккаунта
        /// </summary>
        public DateTime Registrationdate { get; set; }

        /// <summary>
        /// Является ли аккаунт пользователя подтверждённым
        /// </summary>
        public bool IsVerified { get; set; }
    }
}
