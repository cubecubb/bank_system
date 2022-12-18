using bank_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_app
{
    public class Helper
    {
        private static modulesEntities1 modulesEntities;
        public static modulesEntities1 GetContext()
        {
            if (modulesEntities == null)
            {
                modulesEntities = new modulesEntities1();
            }
            return modulesEntities;
        }

        public string FindUsers(string Login, string Password)
        {
            var User = modulesEntities.User.Where(x => x.Login == Login).FirstOrDefault();
            if (User == null)
            {
                return "Такого пользователя не существует в системе.";
            }
            else
            {
                if (Password == User.Password)
                {
                    return "Вход в систему выполнен успешно. Формируем договор..";
                }
                else return "Неправильно введен пароль";
            }

        }
    }
}
