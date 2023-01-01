namespace Coyn.Transaction.Model;

public class TransactionsResponse
{
    public IEnumerable<TransactionInstitutionResponse> AccountTransactions { get; set; }
}