﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class ResetModel
    {
        public string Email { get; set; }
        public string NewPassword{ get; set; }
        public string ConfirmPassword { get; set; }
    }
}
