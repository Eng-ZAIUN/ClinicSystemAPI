using AutoMapper;
using BLL.Model;
using DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager, ITokenService tokenService,
                           IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByNameAsync(model.UserName!);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password!))
            {
                authModel.Message = "User or Password is not found";
                return authModel;
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var jwtToken = _tokenService.CreateToken(user, roles, userClaims);

            authModel.IsAuthenticated = true;
            authModel.UserName = user.UserName!;
            authModel.Email = user.Email!;
            authModel.Role = [.. roles];
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Expiration = jwtToken.ValidTo;
            authModel.Message = "Login successful";
            return authModel;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email!) != null
                || await _userManager.FindByNameAsync(model.UserName!) != null)
            {
                return new AuthModel
                {
                    Message = "UserName or Email already exists",
                    IsAuthenticated = false
                };
            }

            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password!);

            if (!result.Succeeded)
            {
                return new AuthModel
                {
                    Message = string.Join(", ", result.Errors.Select(e => e.Description)),
                    IsAuthenticated = false
                };
            }

            await _userManager.AddToRoleAsync(user, RoleConstants.User);

            var jwtToken = _tokenService.CreateToken(
                  user,
                  [RoleConstants.User], 
                  new List<Claim>()
            );

            return new AuthModel
            {
                UserName = user.UserName!,
                Email = user.Email!,
                IsAuthenticated = true,
                Role = [RoleConstants.User], 
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Expiration = jwtToken.ValidTo,
            };
        }

        public async Task<AuthModel> AssignRoleAsync(AssignRoleModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByIdAsync(model.UserId!);
            if (user == null || !await _roleManager.RoleExistsAsync(model.Role!))
            {
                authModel.Message = "User Id or Role not found";
                return authModel;
            }

            // ✅ تحقق هل المستخدم لديه الدور بالفعل؟
            if (await _userManager.IsInRoleAsync(user, model.Role!))
            {
                authModel.Message = $"User already has the '{model.Role}' role.";
                return authModel;
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role!);
            if (!result.Succeeded)
            {
                authModel.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                return authModel;
            }

            authModel.IsAuthenticated = true;
            authModel.UserName = user.UserName!;
            authModel.Email = user.Email!;
            authModel.Role = new List<string> { model.Role! };
            authModel.Message = "Role assigned successfully";

            return authModel;
        }

    }

}
