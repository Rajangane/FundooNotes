using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public RegisterResponse Registration(UserRegistration user);
        public string Login(UserLogin login);
        public string GenerateJWTToken(string EmailId, long userId);
        public string ForgetPassword(string EmailId);
        public bool ResetPassword(string Email, string ConfirmPassword, string NewPassword);
      
    }
}
