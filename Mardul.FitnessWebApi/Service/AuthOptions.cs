using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mardul.FitnessWebApi.Service
{
    public class AuthOptions
    {
        public const string ISSUER = "MardulWebApi"; // издатель токена
        public const string AUDIENCE = "MardulMobile"; // потребитель токена
        const string KEY = "Fgbt45Esq1!ppoyRTfgdf%ee";   // ключ для шифрации
        public const int LIFETIME = 15; // время жизни токена - 15 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
