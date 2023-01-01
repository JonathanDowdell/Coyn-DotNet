using Coyn.Token.Model;
using Coyn.Transaction.Model;
using Coyn.User.Data;
using Going.Plaid.Item;
using Going.Plaid.Link;

namespace Coyn.Plaid;

public interface IPlaidService
{
    public Task<TransactionsResponse> GetTransactionsAsync(string accessToken, string? cursor);
    public Task<LinkTokenCreateResponse> CreateLinkTokenAsync(UserEntity userEntity);
    public Task<ItemPublicTokenExchangeResponse> ExchangePublicTokenAsync(PlaidExchangeTokenRequest plaidExchangeTokenRequest);
}