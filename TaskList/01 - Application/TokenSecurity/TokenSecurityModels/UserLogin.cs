using System;
using System.Collections.Generic;
using System.Text;
using TaskList._01___Domain;

namespace TaskList._01___Application.TokenSecurity.TokenSecurityModels
{

    public class UserLogin : BaseEntity
    {

        public string Email { get; set; } = "wf@supero.com.br";
        public string Password { get; set; } = "1234";

    }
}


