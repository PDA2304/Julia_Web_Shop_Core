using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Julia_Web_Shop_Core.Models
{
    public class Users: LoginModel
    {
        [Key]
        public Guid id { get; set; }

        [Required(ErrorMessage = "Поле пустое")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле пустое")]
        [MaxLength(10, ErrorMessage = "Пароль слишком длинный, максимальное значение 10 символов")]
        [MinLength(6, ErrorMessage = "Пароль слишком короткий, минимальное значение 6 символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле пустое")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }


    public class Users_Function
    {
        private readonly AppDBContent context;
        public Users_Function(AppDBContent context)
        {
            this.context = context;
        }

        public bool GetByLogin(string Login)
        {
            var log = context.User.Where(p => EF.Functions.Like(p.Login, Login));
            if (log.Count() == 0)
            {
                return true;
            }
            return false;
        }

        public bool GetByEmail(string Email)
        {
            var email = context.User.Where(p => EF.Functions.Like(p.Login, Email));
            if (email.Count() == 0)
            {
                return true;
            }
            return false;
        }

        public Guid SaveArticle(Users entity)
        {
            if (entity.id == default)
            {
                context.Entry(entity).State = EntityState.Added;
                context.User.Add(entity);

            }
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity.id;
        }

    }
}
