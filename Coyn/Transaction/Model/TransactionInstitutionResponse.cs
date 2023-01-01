using Going.Plaid.Entity;

namespace Coyn.Transaction.Model;

public class TransactionInstitutionResponse
{
    public Account? Account { get; set; }
    public string? Cursor { get; set; }
    public IEnumerable<Going.Plaid.Entity.Transaction> Transactions { get; set; }
}