using Coyn.Transaction.Model;
using Coyn.Transaction.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coyn.Transaction.Controller;

[ApiController]
[Route("transaction"), Authorize]
public class TransactionController: Microsoft.AspNetCore.Mvc.Controller
{
   private readonly ITransactionService _transactionService;
   
   public TransactionController(ITransactionService transactionService)
   {
      _transactionService = transactionService;
   }

   [HttpPost]
   public async Task<TransactionsResponse> GetTransactions(TransactionsRequest transactionsRequest)
   {
      return await _transactionService.GetTransactionsAsync(transactionsRequest);
   }
   
}