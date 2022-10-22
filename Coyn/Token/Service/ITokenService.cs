using Coyn.Token.Model;
using Coyn.User.Data;

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
}