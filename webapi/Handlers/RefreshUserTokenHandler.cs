using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using musingo_auth_service.Commands;
using musingo_auth_service.Data;

namespace musingo_auth_service.Handlers;

public class RefreshUserTokenHandler : IRequestHandler<RefreshUserTokenCommand,string>
{
    private readonly IUsersRepository _usersRepository;
    
    public RefreshUserTokenHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task<string> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
    {
        //TODO:Export it somewhere discrete
        string key = "dlkfjg0934u5tdg54g";
            
        var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(key));
        var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
        
        var user = await _usersRepository.GetUserByLoginIdAsync(request.LoginId);

        var permClaims = new List<Claim>();
        permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
        permClaims.Add(new Claim("id",user.UserId.ToString()));

        var token = new JwtSecurityToken(null,
            null, 
            permClaims,    
            expires: DateTime.Now.AddDays(1),    
            signingCredentials: credentials);    
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        user.Token = jwtToken;
        
        //TODO: Handle or log if error - returns false
        await _usersRepository.UpdateUserAsync(user);
        
        return jwtToken;
    }
}