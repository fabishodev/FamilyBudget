using System.Threading.Tasks;
using FamilyBudget.Api.Services.Interfaces;
using FamilyBudget.Api.Repository.Interfaces;
using FamilyBudget.Api.Repository;

using FamilyBudget.Entities;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;



namespace FamilyBudget.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IUserRepository _userRepository;

        public AuthService(AuthRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<User> GetUserBydId(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<UserSession> Auth(Auth auth)
        {
            var session = await _repository.Auth(auth);

            if(session.UserId > 0)
            {
                session.Token = GenerateJwtToken(session);
            }

            return session;
        }

        public string GenerateJwtToken(UserSession session)
        {
           var secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACTE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";  
            
 
            var tokenHandler = new JwtSecurityTokenHandler();
 
            var key = System.Text.Encoding.ASCII.GetBytes(secret);
 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", session.UserId.ToString())}) , 
                Expires = System.DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
                
            };
 
            var token = tokenHandler.CreateToken(tokenDescriptor);
 
            return tokenHandler.WriteToken(token);

            
        }
    }
}