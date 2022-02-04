using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.AppContexts;
using RepositoryLayer.Entites;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        Context context;
        private readonly IConfiguration configuration;

        public UserRL(Context context,IConfiguration config)
        {
            this.context = context;
            this.configuration = config;
        }
        public bool Registration(UserRegistration user)
        {
            try
            {

                User newuser = new User();
                newuser.FirstName = user.Firstname;
                newuser.LastName = user.Lastname;
                newuser.Email = user.Email;
               // newuser.Password = user.Password;
                newuser.Password = EncryptPassword(user.Password);
                context.Users.Add(newuser);
                int result = context.SaveChanges();
                if(result > 0)
                {
                    return true;
                 
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string EncryptPassword(string password)
        {
            try
            {
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                string encPassword = Convert.ToBase64String(encode);
                return encPassword;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string DecryptPassword(string encryptpwd)
        {
            try
            {
                UTF8Encoding encodepwd = new UTF8Encoding();
                Decoder Decode = encodepwd.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
                int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string decryptpwd = new String(decoded_char);
                return decryptpwd;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
                User user = new User();
                user = context.Users.Where(x => x.Email == userLogin.Email).FirstOrDefault();
                string decPass = DecryptPassword(user.Password);
                var id = user.Id;
                if (decPass == userLogin.Password && user != null)
                    return ClaimTokenByID(id);
                else
                    return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ClaimTokenByID(long Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateJWTToken(string email, long userId)
        {
            try
            {
                var loginTokenHandler = new JwtSecurityTokenHandler();
                var loginTokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var loginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new []
                {
                new Claim(ClaimTypes.Email, email),
                new Claim("Id", userId.ToString())
                }),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(loginTokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = loginTokenHandler.CreateToken(loginTokenDescriptor);
                return loginTokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
        }
     
        public string ForgetPassword(string EmailId)
        {
            try
            {
                var Email = context.Users.FirstOrDefault(e => e.Email == EmailId);  
                if(Email != null)
                {
                    var token = GenerateJWTToken(Email.Email,Email.Id);
                       new MSMQModel().MsmqSender(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }

            catch (Exception)
            {

                throw;
            }

        }

        public bool ResetPassword(string Email, string ConfirmPassword, string NewPassword)
        {
            try
            {
                if (NewPassword.Equals(ConfirmPassword))
                {
                    User user = context.Users.Where(e => e.Email == Email).FirstOrDefault();
                    user.Password = ConfirmPassword;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
           
        }
       
    }
}
