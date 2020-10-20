using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityService.Core.DTOs;
using IdentityService.Core.DTOs.Account;
using IdentityService.Core.Entities;
using IdentityService.Persistence.Interfaces;
using IdentityService.Services.Interfaces.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Services.Account
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration config, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Login(LoginDto loginDto)
        {
           var islogin = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.PasswordHash, false, false);
            if (!islogin.Succeeded) throw new Exception("Invalid Username or Password! ");
           
                var result = new { Token = await GenerateJSONWebTokenAsync(loginDto) };
                return result;
        }

        public async Task<object> AssignRoles(AssignRoleDto assignRoleDto)
        {
            var user = await _userManager.FindByIdAsync(assignRoleDto.UserId);

            if (user == null || assignRoleDto.Role == null)
                throw new Exception("User Not Found! ");
            if (!(await _userManager.IsInRoleAsync(user, assignRoleDto.Role)))
            {
                var result = await _userManager.AddToRoleAsync(user, assignRoleDto.Role);
                return result;
            }
            else throw new Exception("Role Already Assigned! ");
        }

        public async Task<object> Logout()
        {
            await _signInManager.SignOutAsync();
            return "Logged out Successfully";
        }

        public async Task<object> Register(RegisterDto signUpDto)
        {
            try
            {
                var user = new ApplicationUser { UserName = signUpDto.UserName, Email = signUpDto.Email };
                var result = await _userManager.CreateAsync(user, signUpDto.Password);
                if (!result.Succeeded) throw new Exception("Register not completed ");
             return result;
            }
            catch (Exception ex)
            {
                return new { message = ex.Message };
            }
        }

        public async Task<PagedResultDto<LoginDto>> GetPagedUsers(int? pageIndex, int? pageSize)
        {
            PagedResultDto<ApplicationUser> user = await _unitOfWork.AccountRepository.GetAllIncludedPagnation(U => U.UserName != null, pageIndex, pageSize);
            PagedResultDto<LoginDto> result = _mapper.Map<PagedResultDto<ApplicationUser>, PagedResultDto<LoginDto>>(user);
            return result;
        }

        public async Task<LoginDto> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new Exception("User Not Found! ");
            var result = _mapper.Map<ApplicationUser, LoginDto>(user);
            return result;
        }

        private async Task<string> GenerateJSONWebTokenAsync(LoginDto loginDto )
        {

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:SecurityKey"])), SecurityAlgorithms.HmacSha256);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

           foreach(var role in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }
         
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(type: "Username", user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.Ticks.ToString(), ClaimValueTypes.Integer64)
            }.Union(userClaims);

             var token = new JwtSecurityToken(
                 _config["Jwt:Issuer"],
                 _config["Jwt:Audience"],
                 claims,
                 notBefore: DateTime.Now,
                 expires: DateTime.Now.AddDays(1),
                 signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}