using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinancasApp.Configurations;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Controllers.V1.Dtos.Response;
using FinancasApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FinancasApp.Services;

public class IdentityService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<JwtOptions> jwtOptions)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<ResponseBase<SimpleUserResponse>> CreateNewUser(CreateNewUserRequest request)
    {
        var identityUser = new User
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };

        var response = new ResponseBase<SimpleUserResponse>();

        var result = await _userManager.CreateAsync(identityUser, request.Senha);
        if (result.Succeeded)
        {
            await _userManager.SetLockoutEnabledAsync(identityUser, false);
            response.Success = new SimpleUserResponse(identityUser);
        }

        if (result.Errors.Count() > 0)
        {
            response.SetErros(result.Errors.Select(er => er.Description));
        }

        return response;
    }

    public async Task<ResponseBase<object>> Login(UserLoginRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);

        var response = new ResponseBase<object>();
        if (result.Succeeded)
        {
            var token = await GenerateToken(request.Email);
            response.Success = new { token };
        }

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                response.AddError("Conta bloqueada");
            else if (result.IsNotAllowed)
                response.AddError("Essa conta não tem permissão para essa ação");
            else if (result.RequiresTwoFactor)
                response.AddError("É necessário confirmar o login com o código de 2 fatores");
            else
                response.AddError("E-mail or password invalid");
        }

        return response;
    }

    private async Task<string> GenerateToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var tokenClaims = await GetClaims(user);

        var expDate = DateTime.Now.AddSeconds(_jwtOptions.Expiration);
        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: tokenClaims,
            notBefore: DateTime.Now,
            expires: expDate,
            signingCredentials: _jwtOptions.SigningCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return token;
    }

    private async Task<IList<Claim>> GetClaims(User user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

        foreach (var role in roles)
            claims.Add(new Claim("role", role));

        return claims;
    }

}
