using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL UserRL;
        public UserBL(IUserRL UserRL)
        {
            this.UserRL = UserRL;
        }
        public bool Registration(UserRegistration user)
        {
            try
            {
                return UserRL.Registration(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLogin login)
        {
          
            try
            {
                return UserRL.Login(login);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public string GenerateJWTToken(string EmailId, long userId)
        {
            try
            {
                return UserRL.GenerateJWTToken(EmailId,userId);
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
                return UserRL.ForgetPassword(EmailId);
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
                return UserRL.ResetPassword(Email, ConfirmPassword,NewPassword);
           
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
