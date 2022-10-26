using Microsoft.AspNetCore.Http;

namespace FluxoCaixa.Chassis.Utils.Common
{
    public static class AppResults
    {
        public static IResult Ok(object data)
        {
            var response = new DefaultResponse<object>(data);
            return Results.Ok(response);
        }

        public static IResult Created(string uri, object data)
        {
            var response = new DefaultResponse<object>(data);
            return Results.Created(uri, response);
        }

        public static IResult NoContent()
        {
            return Results.NoContent();
        }
    }
}
