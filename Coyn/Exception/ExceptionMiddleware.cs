using System.Net;

namespace Coyn.Exception;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary> The InvokeAsync function is a helper function that is used to call the next middleware in the pipeline.
    /// It also handles exceptions thrown by any middleware and converts them into appropriate HTTP responses.</summary>
    ///
    /// <param name="httpContext"> The http context.</param>
    ///
    /// <returns> A task.</returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (CoynException exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)exception.StatusCode;

            Console.WriteLine(exception);

            await httpContext.Response.WriteAsync(new ErrorDetails(exception).ToString());
        }
        catch (System.Exception exception)
        {
            Console.WriteLine(exception);
            
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    /// <summary> The HandleExceptionAsync function is a custom middleware that handles exceptions thrown by the application.
    /// It is used to return JSON error messages to the client.</summary>
    ///
    /// <param name="context"> </param>
    /// <param name="System.Exception"> </param>
    ///
    /// <returns> An async task.</returns>
    private async Task HandleExceptionAsync(HttpContext context, System.Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error from the custom middleware."
        }.ToString());
    }
}