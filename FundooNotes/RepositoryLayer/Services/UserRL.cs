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
        contexts context;
        private readonly IConfiguration configuration;

        public UserRL(contexts context,IConfiguration config)
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
                newuser.Password = user.Password;   
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
        public bool Login(UserLogin login)
        {
            try
            {
                User newuser = new User();
                var result = context.Users.Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefault();
                

                if (result != null)
                {
                    string token = "";
                    UserResponse loginResponse = new UserResponse();
                    token = GenerateJWTToken(login.Email);
                    //loginResponse.Id = ValidLogin.Id;
                    loginResponse.EmailId = login.Email;
                    loginResponse.Token = token;

                    
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

        public string GenerateJWTToken(string EmailId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
           new Claim(ClaimTypes.Email,EmailId),
           };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], EmailId,
              claims,
              expires: DateTime.Now.AddMinutes(20),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
        public string ForgetPassword(string EmailId)
        {
            try
            {
                var Email = context.Users.FirstOrDefault(e => e.Email == EmailId);  
                if(Email != null)
                {
                    var token = GenerateJWTToken(Email.Email);
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
