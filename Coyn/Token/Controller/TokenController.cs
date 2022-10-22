using Coyn.Plaid;
using Coyn.Token.Model;
using Coyn.Token.Service;
using Coyn.User.Service;
using Going.Plaid.Link;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coyn.Token.Controller;

[ApiController]
[Route("token"), Authorize]
public class TokenController: Microsoft.AspNetCore.Mvc.Controller
{

    private readonly IPlaidService _plaidService;
    
    private readonly ITokenService _tokenService;

    private readonly IUserService _userService;
    
    public TokenController(ITokenService tokenService, IPlaidService plaidService, IUserService userService)
    {
        _tokenService = tokenService;
        _plaidService = plaidService;
        _userService = userService;
    }

    /// <summary> The RefreshToken function exchanges a refresh token for an access token.</summary>
    ///
    /// <param name="refreshTokenRequest"> Refreshtokenrequest</param>
    ///
    /// <returns> A tokenresponse object.</returns>
    [HttpPost("refresh"), AllowAnonymous]
    public async Task<TokenResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
        return await _tokenService.ExchangeRefreshToken(refreshTokenRequest.RefreshToken);
    }

    /// <summary> The CreateLinkToken function creates a link token for the current user.</summary>
    ///
    /// <returns> A linktokencreateresponse object.</returns>
    [HttpPost("link")]
    public async Task<LinkTokenCreateResponse> CreateLinkToken()
    {
        var userEntity = await _userService.GetCurrentUserAsync();
        return await _plaidService.CreateLinkToken(userEntity);
    }
}