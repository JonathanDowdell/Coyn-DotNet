using System.Net;
using Coyn.Exception;

namespace Coyn.Extension;

public static class GuidExtensions
{
    public static Guid ParseOrThrow(string stringId)
    {
        try
        {
            return Guid.Parse(stringId);
        }
        catch (System.Exception e)
        {
            throw new CoynException(HttpStatusCode.BadRequest, $"ID {stringId} is invalid.");
        }
    }
}