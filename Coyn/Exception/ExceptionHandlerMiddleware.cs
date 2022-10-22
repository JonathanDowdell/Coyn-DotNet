using System.Net;

namespace Coyn.Exception;

public class ExceptionHandlerMiddleware: IMiddleware
{
    /// <summary> The InvokeAsync function is a middleware function that is used to catch exceptions thrown by the next delegate.
    /// It also sets the response status code to be returned from this middleware.</summary>
    ///
    /// <param name="context"> The http context.</param>
    /// <param name="RequestDelegate"> The delegate that is invoked for each request.</param>
    ///
    /// <returns> An awaitable task.</returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (System.Exception e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await context.Response.WriteAsync(e.Message);
        }
    }
}