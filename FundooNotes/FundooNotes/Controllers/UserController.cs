using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL UserBL;
        public UserController(IUserBL UserBL)
        {
            this.UserBL = UserBL;

        }

        
        [HttpPost("Register")]
        public IActionResult AddUser(UserRegistration userRegistration)
        {
            try
            {
                RegisterResponse respo = this.UserBL.Registration(userRegistration);
                if(respo.Email == userRegistration.Email)

                {
                    return this.Ok(new { Success = true, message = "Registration Sucessfull",data = respo });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsucessfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
       
        [HttpPost("Login")]
        public IActionResult AddLogin(UserLogin userLogin)
        {
            try
            {
                var token = UserBL.Login(userLogin);
                if (token != null)

                {
                    return this.Ok(new { Success = true, message = "login Sucessfull",Token=token });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "login Unsucessfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost("Reset")]
        public IActionResult ResetPassword(string Email, string ConfirmPassword, string NewPassword)
        {
            try
            {
                if (UserBL.ResetPassword(Email,ConfirmPassword,NewPassword))

                {
                    return this.Ok(new { Success = true, message = "Reset Sucessfull" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Reset fail" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost("Forgot")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                
                 UserBL.ForgetPassword(Email);
                    return this.Ok(new { Success = true, message = "Forget Sucessfull" });
               
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    

    }
}
