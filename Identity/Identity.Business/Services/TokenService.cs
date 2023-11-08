using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Identity.Business.Extensions;
using Identity.Business.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.Business.Services;

public class TokenService: ITokenService
{
	private readonly IConfiguration _configuration;
	private UserManager<IdentityUser<long>> _userManager;
	public TokenService
		(IConfiguration configuration, 
		UserManager<IdentityUser<long>> userManager)
	{
		_configuration = configuration;
		_userManager = userManager;		
	}
	
	private async Task<List<Claim>> GetClaimsAsync(IdentityUser<long> user)
	{
		var claims = new List<Claim>
		{
			new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
		};

		var role = (await _userManager.GetRolesAsync(user)).First();
		claims.Add(new Claim(ClaimTypes.Role, role));

		return claims;
	}

	private SecurityTokenDescriptor GetTokenDescriptor(List<Claim> claims, SigningCredentials creds)
	{
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.Now.AddDays(1),
			SigningCredentials = creds,
			Audience = _configuration["Jwt:Audience"],
			Issuer = _configuration["Jwt:Issuer"],
		};

		return tokenDescriptor;
	}

	public async Task<string> GetTokenAsync(IdentityUser<long> user)
	{
		var claims = await GetClaimsAsync(user);
		var key = new SymmetricSecurityKey(
			Encoding.Default.GetBytes(_configuration["Jwt:Secret"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
		var tokenDescriptor = GetTokenDescriptor(claims, creds);
		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}
	
}