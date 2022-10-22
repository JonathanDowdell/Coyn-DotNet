using Coyn.User.Data;
using Going.Plaid.Item;
using Going.Plaid.Link;

namespace Coyn.Plaid;

public interface IPlaidService
{
    public Task<IEnumerable<Going.Plaid.Entity.Transaction>> GetTransactions(string accessToken, string? cursor);
    public Task<LinkTokenCreateResponse> CreateLinkToken(UserEntity userEntity);
    public Task<ItemPublicTokenExchangeResponse> ExchangePublicToken();
}