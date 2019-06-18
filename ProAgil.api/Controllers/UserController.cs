using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using System.Threading.Tasks;
using ProAgil.api.dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace ProAgil.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _sign;
        public UserController(IConfiguration config,UserManager<User> userManager,SignInManager<User> sign,IMapper mapper){
            this.config= config;
            this._mapper= mapper;
            this._userManager= userManager;
            this._sign= sign;
        }

        [HttpGet("GetUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(UserDTO user){
            return Ok(user);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDTO User){
            try{
                var user= _mapper.Map<User>(User);
                var retorno= await _userManager.CreateAsync(user,User.Password);
                var userRetornar= _mapper.Map<UserDTO>(user);
                if(retorno.Succeeded){
                    return Created("GetUser",userRetornar);
                }
                return BadRequest(retorno.Errors);
            }catch(System.Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"{ex}");
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO UserLogin){
            try{
                var user= await _userManager.FindByNameAsync(UserLogin.UserName);
                var result= await _sign.CheckPasswordSignInAsync(user,UserLogin.Password,false);
                if(result.Succeeded){
                    var appUser= await _userManager.Users.FirstOrDefaultAsync(u=> u.NormalizedUserName == UserLogin.UserName.ToUpper());
                    var userReturn= _mapper.Map<UserLoginDTO>(appUser);
                    return Ok(new{
                        token= GerarToken(appUser).Result,
                        user= userReturn,
                    });
                }
                return Unauthorized();
            }catch(System.Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"{ex}");
            }
        }
        public async Task<string> GerarToken(User user){
            
            var claim= new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
            };
            var roles= await _userManager.GetRolesAsync(user);
            foreach(var r in roles){
                claim.Add(new Claim(ClaimTypes.Role,r));
            }
            var key= new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                        config.GetSection("AppSettings:Token").Value
                    ));
            var cred= new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription= new SecurityTokenDescriptor{
                Subject= new ClaimsIdentity(claim),
                Expires= DateTime.Now.AddDays(1),
                SigningCredentials= cred,
            };
            var tokenHandler= new JwtSecurityTokenHandler();
            var token= tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }

    }
}