using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EDMSModel
{
    public class ReturnMessageModel
    {
        public ReturnMessageModel()
        {
            Success = false;
            Msg = "Error";
        }
        public bool Success { get; set; }
        public bool SuccessOne{get;set;}
        public string Msg { get; set; }
        public int ReturnId { get; set; }
        public decimal ValueOne { get; set; }
    }

}