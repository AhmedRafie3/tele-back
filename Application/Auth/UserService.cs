using Application.DTO;
using Application.Repository.IBase;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TeleperformanceTask.Auth
{
    public class UserService(JwtOptions options, IUnitOfWork unitOfWork)
    {
        public async Task<RetuenAuth> AuthenticateUser(AuthenticationRequest request)
        {
            var ress = new RetuenAuth();
            var data = unitOfWork.Repository<User>().FindByCondition(s => s.UserName == request.UserName && s.Password == request.Password).FirstOrDefault();
            if (data == null) { ress.Message = "You Daont Have Account"; return ress; }

            var tokenHandeler = new JwtSecurityTokenHandler();
            var tokenDescriptor =new SecurityTokenDescriptor();
            if (data.UserRole == 1)
            {
                 tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = options.Issuer,
                    Audience = options.Audance,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),
               SecurityAlgorithms.HmacSha256),

                    Subject = new ClaimsIdentity(new Claim[]
               {
                    new(ClaimTypes.NameIdentifier, request.UserName),
                    new(ClaimTypes.Email, request.UserName),
                    new(ClaimTypes.Role, "Admin")
               }),
                    Expires = DateTime.UtcNow.AddHours(1) 
                };
            }
            else
            {
                 tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = options.Issuer,
                    Audience = options.Audance,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),
               SecurityAlgorithms.HmacSha256),

                    Subject = new ClaimsIdentity(new Claim[]
               {
                    new(ClaimTypes.NameIdentifier, request.UserName),
                    new(ClaimTypes.Email, request.UserName),
                    new(ClaimTypes.Role, "User")
               }),
                    Expires = DateTime.UtcNow.AddHours(1) 
                };
            }
           
            var securityToken = tokenHandeler.CreateToken(tokenDescriptor);
            var accesstoken = tokenHandeler.WriteToken(securityToken);
            
            ress.Token= accesstoken;
            ress.UserRole = data.UserRole;
            return ress;
        }

        public async Task<bool> RegisterUser(RegisterationRequest request, CancellationToken cancellationToken)
        {
            var data = unitOfWork.Repository<User>().FindByCondition(s => s.UserName == request.UserName && s.Password == request.Password).ToList();
            if (data.Count > 0) return false;

            var user = new User();
            user.UserRole = request.UserRole;
            user.Password = request.Password;
            user.UserName = request.UserName;

            unitOfWork.Repository<User>().Create(user);
            var res = await unitOfWork.CompleteAsync(cancellationToken);
            return res == 1 ? true : false;
        }
    }
}
