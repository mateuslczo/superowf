using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using TaskList._01___Application.TokenSecurity;
using TaskList._01___Application.TokenSecurity.TokenSecurityModels;
using TaskList._01___Application.ViewModels;

namespace TaskListWeb.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("v1")]
    public class AuthController : Controller
    {

        /// <summary>
        /// Gerar token e autenticar usuário
        /// </summary>
        [HttpPost("authentication")]
        public ActionResult<UserAccessViewModel> Post([FromServices] TokenConfig tokenConfig,
                                                      [FromServices] UserLogin user,
                                                      [FromServices] TokenSecurityConfig tokenSecurityConfig,
                                                      UserAccessViewModel authModel)
        {

            if (String.IsNullOrEmpty(authModel.Email) || String.IsNullOrEmpty(authModel.Password))
            {
                return BadRequest(new { error = "Authentication failed" });

            } else
            {

                bool validCredentials = (user != null && authModel.Email == user.Email && authModel.Password == user.Password);

                if (validCredentials)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(authModel.Email, "Login"),
                        new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, authModel.Email)
                        });

                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = dataCriacao +
                        TimeSpan.FromSeconds(tokenConfig.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfig.Issuer,
                        Audience = tokenConfig.Audience,
                        SigningCredentials = tokenSecurityConfig.Credentials,
                        Subject = identity,
                        NotBefore = dataCriacao,
                        Expires = dataExpiracao
                    });

                    var token = handler.WriteToken(securityToken);

                    var result = new
                    {
                        AccessToken = token,
                        created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                        expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                        authenticated = true
                    };
                    return Ok(result);


                } else
                {
                    var result = new
                    {
                        AccessToken = "",
                        authenticated = false
                    } ;


                    return Ok(result);

                }
            }
        }
    }
}
