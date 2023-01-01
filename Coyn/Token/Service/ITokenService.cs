using Coyn.Token.Model;
using Coyn.User.Data;
using Going.Plaid.Item;
using Going.Plaid.Link;

namespace Coyn.Token.Service;

public interface ITokenService
{
    /// <summary> The CreateServerToken function creates a JWT token for the user.</summary>
    /// <param name="currentUser"> </param>
    /// <returns> A tokenResponse object.</returns>
    Task<TokenResponse> CreateServerToken(UserEntity currentUser);
    
    /// <summary> The ExchangeRefreshToken function exchanges a refresh token for a new access token.</summary>
    ///
    /// <param name="refreshToken"> The refresh token.</param>
    ///
    /// <returns> A tokenresponse object.</returns>
    Task<TokenResponse> ExchangeRefreshToken(string refreshToken);

    /// <summary> The ExchangePublicToken function exchanges a public token for an access token.</summary>
    /// 
    /// <param name="plaidExchangeTokenRequest"></param>
    /// 
    /// <returns> An itempublictokenexchangeresponse object.</returns>
    Task<ItemPublicTokenExchangeResponse> ExchangePlaidPublicTokenAsync(PlaidExchangeTokenRequest plaidExchangeTokenRequest);
    
    /// <summary> The CreateLinkToken function creates a link token for the user.</summary>
    ///
    /// <param name="userEntity"> The user entity</param>
    ///
    /// <returns> A linktokencreateresponse object.</returns>
    public Task<LinkTokenCreateResponse> CreatePlaidLinkTokenAsync(UserEntity userEntity);
}