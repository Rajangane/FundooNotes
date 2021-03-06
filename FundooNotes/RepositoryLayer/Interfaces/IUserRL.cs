using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public RegisterResponse Registration(UserRegistration user);
        public string Login(UserLogin login);
        public string GenerateJWTToken(string EmailId, long userId);
        public string ForgetPassword(string EmailId);
        public bool ResetPassword(string Email, string ConfirmPassword, string NewPassword);
    }
}
