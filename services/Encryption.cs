using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace jwt.services
{
    public class Encryption
    {
        public string createEncryptPassword(string unhashedPassword)
        {
            string hashedPassword = Crypto.HashPassword(unhashedPassword);
            return hashedPassword;
        }

        public bool checkEncryptPassword(string savedHashedPassword, string unhashedPassword)
        {
            if (Crypto.VerifyHashedPassword(savedHashedPassword, unhashedPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
