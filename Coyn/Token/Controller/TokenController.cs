using Coyn.Token.Model;
using Coyn.Token.Service;
using Coyn.User.Service;
using Going.Plaid.Item;
using Going.Plaid.Link;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coyn.Token.Controller;

[ApiController]
[Route("token"), Authorize]
public class TokenController: Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ITokenService _tokenService;

    private readonly IUserService _userService;
    
    public TokenController(ITokenService tokenService, IUserService userService)
    {
        _tokenService = tokenService;
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
    [HttpPost("plaid/link")]
    public async Task<LinkTokenCreateResponse> CreateLinkToken()
    {
        var userEntity = await _userService.GetCurrentUserAsync();
        return await _tokenService.CreatePlaidLinkTokenAsync(userEntity);
    }

    /// <summary> The ExchangePlaidToken function exchanges a public token for an access token.</summary>
    ///
    /// <param name="plaidExchangeTokenRequest"> The public token returned by plaid's /item/public_token/exchange endpoint.</param>
    ///
    /// <returns> An itempublictokenexchangeresponse object.</returns>
    [HttpPost("plaid/exchange")]
    public async Task<ItemPublicTokenExchangeResponse> ExchangePlaidToken(PlaidExchangeTokenRequest plaidExchangeTokenRequest)
    {
        return await _tokenService.ExchangePlaidPublicTokenAsync(plaidExchangeTokenRequest);
    }
}