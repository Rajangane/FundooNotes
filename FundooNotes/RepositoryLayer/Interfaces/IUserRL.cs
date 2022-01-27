using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public bool Registration(UserRegistration user);
        public bool Login(UserLogin login);
        public string GenerateJWTToken(string EmailId);
        public string ForgetPassword(string EmailId);
        public bool ResetPassword(ResetModel EmailId);
    }
}
