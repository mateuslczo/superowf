using System;
using System.Collections.Generic;
using System.Text;

namespace TaskList._01___Application.TokenSecurity.TokenSecurityModels
{

    public class TokenConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }

}