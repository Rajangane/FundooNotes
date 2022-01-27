using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailId { get; set; }
        public string Token { get; set; }
    }
}
