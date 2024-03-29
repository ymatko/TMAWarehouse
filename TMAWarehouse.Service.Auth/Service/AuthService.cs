﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TMAWarehouse.Service.Auth.Models.Dto;
using TMAWarehouse.Service.Auth.Service.IService;
using TMAWarehouse.Services.Auth.Data;

namespace TMAWarehouse.Service.Auth.Service
{
	public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;

        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == email.ToLower());

            if(user != null)
            {
                if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

		public async Task<IdentityUser> ChangeRole(string email, string roleName)
		{
            var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == email.ToLower());

            if(user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, currentRoles);

                await _userManager.AddToRoleAsync(user, roleName);
                return user;
            }
            return new IdentityUser();
		}

		public async Task<string> GetRole(string userId)
		{
			var user = _db.Users.FirstOrDefault(u => u.Id == userId);

			if (user == null)
			{
				return string.Empty;
			}

			var userRole = _db.UserRoles
				.Where(ur => ur.UserId == userId)
				.Join(_db.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r)
				.FirstOrDefault();

			if (userRole == null)
			{
				return string.Empty;
			}

			return userRole.Name;
		}

		public async Task<IdentityUser> GetUser(string userId)
		{
			var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

			if (user != null)
			{
				return user;
			}
			return new IdentityUser();
		}

		public async Task<IEnumerable<IdentityUser>> GetUsers()
        {
            IEnumerable<IdentityUser> users;
            try
            {
                users = await _db.Users.ToListAsync();
                if(users == null)
                {
                    return Enumerable.Empty<IdentityUser>();
                }
            }
            catch(Exception ex)
            {
                users = Enumerable.Empty<IdentityUser>();
            }
            return users;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if(user == null || !isValid)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.UserName,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            IdentityUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedUserName = registrationRequestDto.Name.ToUpper(),
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.Users.First(u => u.NormalizedEmail == registrationRequestDto.Email.ToUpper());

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.UserName,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }
    }
}
