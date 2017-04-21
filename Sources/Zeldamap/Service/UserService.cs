using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using zeldassistant.Data;
using zeldassistant.Models;
 
namespace zeldassistant
{
    public class UserService
    {
        private ZeldaDataContext _context;
        private DbSet<User> _users { get { return _context.Users; } }

        public UserService(ZeldaDataContext context)
        {
            this._context = context;

        }

        public User GetByLogin(string login)
        {
            return _users.FirstOrDefault(u => u.Login.ToLower() == login.ToLower());
        }

        public User Validate(string login, string password)
        {
            User user = _users.FirstOrDefault(u => u.Login.ToLower() == login.ToLower());
            if (user == null)
            {
                return null;
            }


            /* Fetch the stored value */
            string savedPasswordHash = user.PasswordHash;
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    throw new UnauthorizedAccessException();
                }

            }

            return user;
        }

        public async Task<User> Insert(User m)
        {
            if (GetByLogin(m.Login) != null)
            {
                return null;
            }
            byte[] salt;
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt = new byte[16]);
            // new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(m.PasswordHash, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            m.PasswordHash = savedPasswordHash;
            _users.Add(m);
            await _context.SaveChangesAsync();

         


            return m;
        }

        internal async Task<User> CreateNewUser(User u,MarkerService markerService)
        {
            u = await this.Insert(u);
            if (u== null)
            {
                return null;
            }
//insert default markers
            //JsonDataLoader loader = new JsonDataLoader();
            //var builtinMarker = loader.Load();
            //foreach (var marker in builtinMarker)
            //{
            //    marker.UserId = u.Id;
            //}
            //markerService.AddRange(builtinMarker);
            return u;
        }
    }
}
