using _15PuzzleGame.Entities;
using _15PuzzleGame.Interfaces;
using _15PuzzleGame.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace _15PuzzleGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly PuzzleContext _puzzleContext;
        private readonly ITokenService _tokenService;

        public AuthController(PuzzleContext puzzleContext, ITokenService tokenService)
        {
            _puzzleContext = puzzleContext;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> LogIn(LoginModel logInModel)
        {
            var user = await _puzzleContext.Users
                .SingleOrDefaultAsync(u => u.Username == logInModel.Username);

            if (user == null)
            {
                return Ok(await Register(logInModel));
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logInModel.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid password");
                }
            }

            var userModel = new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
            return Ok(userModel);
        }

        private async Task<UserModel> Register(LoginModel logInModel)
        {
            using var hmac = new HMACSHA512();

            var user = new MyUser
            {
                Username = logInModel.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logInModel.Password)),
                PasswordSalt = hmac.Key
            };

            await _puzzleContext.AddAsync(user);
            await _puzzleContext.SaveChangesAsync();
            return new UserModel
            {
                Id = user.Id,
                Username = logInModel.Username,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}






