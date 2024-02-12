using System.Net;

namespace TesteMiddleware.api.HttpService
{
 public class ErrorHttpService
    {
        public object GetErrorDetails(Exception ex)
        {
            if (ex.InnerException != null)
            {
                switch (ex.GetType().Name)
                {
                    case "NotFoundException":
                        if (ex.InnerException.Message == "Produto não disponível no estoque.")
                            return new { error = ex.InnerException.Message, code = "ESTOUT401" };
                        else
                            return new { error = ex.InnerException.Message };

                    case "ConflictException":
                    case "ForbiddenException":
                    case "NoContentException":
                    case "BadRequestException":
                    case "InternalServerErrorException":
                    case "ContainHTMLException":
                        return new { error = ex.InnerException.Message };
                    default:
                        return new { error = ex.InnerException.Message };
                }
            }
            return new { error = ex.Message };
        }

        public object GetErrorDetails(string message)
        {
            return new { error = message };
        }

        public int GetHttpStatusCode(Exception ex)
        {
            if (ex.InnerException != null)
            {
                switch (ex.GetType().Name)
                {
                    case "UnauthorizedException":
                        return (int)HttpStatusCode.Unauthorized;
                    case "NotFoundException":
                        return (int)HttpStatusCode.NotFound;
                    case "ConflictException":
                    case "ForbiddenException":
                    case "NoContentException":
                    case "BadRequestException":
                    case "InternalServerErrorException":
                        return (int)HttpStatusCode.InternalServerError;
                    default:
                        return (int)HttpStatusCode.NotFound;
                }
            }
            else
                return (int)HttpStatusCode.NotFound;
        }

        public int GetHttpStatusCode(int errorCode)
        {
            if (errorCode > 0)
            {
                switch (errorCode)
                {
                    case 404:
                        return (int)HttpStatusCode.NotFound;
                    case 409:
                        return (int)HttpStatusCode.Conflict;
                    case 403:
                        return (int)HttpStatusCode.Forbidden;
                    case 204:
                        return (int)HttpStatusCode.NoContent;
                    case 400:
                        return (int)HttpStatusCode.BadRequest;
                    case 500:
                        return (int)HttpStatusCode.InternalServerError;
                    default:
                        return (int)HttpStatusCode.InternalServerError;
                }
            }
            else
                return (int)HttpStatusCode.InternalServerError;
        }
    }
}
