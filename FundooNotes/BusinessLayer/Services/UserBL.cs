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
        public bool Login(UserLogin login)
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
        public string GenerateJWTToken(string EmailId)
        {
            try
            {
                return UserRL.GenerateJWTToken(EmailId);
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

        public bool ResetPassword(ResetModel EmailId)
        {
            try
            {
                return UserRL.ResetPassword(EmailId);
            

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
