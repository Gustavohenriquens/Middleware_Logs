using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TesteMiddleware.api.ErrosHTTP;
using TesteMiddleware.api.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static TesteMiddleware.api.ViewModel.ErrorResponseVm;


namespace TesteMiddleware.api.Middleware
{
    public class ErrorMiddleware
    {

        private readonly RequestDelegate next;
        private readonly ILogger<ErrorMiddleware> _logger;

        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string traceId = Guid.NewGuid().ToString();
            _logger.LogError(ex, $"Erro ocorrido com TraceId: {traceId}");

            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path,
                Type = "https://learn.microsoft.com/pt-br/troubleshoot/developer/webapps/iis/www-administration-management/http-status-code",
                Extensions =
                        {
                            { "traceId", traceId },
                            { "Logref", Guid.NewGuid().ToString() },
                            { "Message", "Messagem padrão que não é especifica do erro"}
                        }
            };

            int statusCode;
            ErrorResponseVm errorResponseVm;

            switch (ex)
            {
                //HTTP 400
                case ArgumentException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorResponseVm = new ErrorResponseVm("ErroID400", "Requisição inválida.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 400....explicando..";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://learn.microsoft.com/pt-br/iis/troubleshoot/diagnosing-http-errors/troubleshooting-http-400-errors-in-iis";
                    break;

                //HTTP 401
                case UnauthorizedAccessException _:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponseVm = new ErrorResponseVm("ErroID401", "Não autorizado.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 401....explicando o erro melhor..";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://www.hostinger.com.br/tutoriais/erro-401#:~:text=O%20Erro%20401%20indica%20um,exigem%20um%20login%20para%20acesso.";
                    break;

                //HTTP 402
                case PaymentRequiredException _:
                    statusCode = (int)HttpStatusCode.PaymentRequired;
                    errorResponseVm = new ErrorResponseVm("ErroID402", "Pagamento necessário.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 402....explicando o erro melhor.. O pagamento....";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://kinsta.com/pt/base-de-conhecimento/http-402/#:~:text=O%20código%20HTTP%20402%20ou,“experimental”%20ou%20em%20desenvolvimento.";
                    break;

                //HTTP 403
                case ForbiddenException _:
                    statusCode = (int)HttpStatusCode.Forbidden;
                    errorResponseVm = new ErrorResponseVm("ErroID403", "Acesso proibido.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 403....explicando o erro melhor.. Acesso foi proibido...";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://www.hostinger.com.br/tutoriais/o-que-significa-erro-403";
                    break;

                //HTTP 404
                case NotFoundException _:
                    statusCode = (int)HttpStatusCode.NotFound;
                    errorResponseVm = new ErrorResponseVm("ErroID404", "Recurso não encontrado.");
                    problemDetails.Extensions["Message"] = "Erro 404....explicando o erro melhor.. O erro não foi encontrado porque falta inserir um id válido";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://www.hostinger.com.br/tutoriais/erro-404";
                    break;

                //HTTP 409
                case ConflictException _:
                    statusCode = (int)HttpStatusCode.Conflict;
                    errorResponseVm = new ErrorResponseVm("ErroID409", "Conflito");
                    problemDetails.Extensions["Message"] = "Erro 409, Deu um conflito em algo..";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://cloud.ibm.com/docs/vpc?topic=vpc-troubleshoot-lb-409&locale=pt-BR#:~:text=O%20código%20de%20status%20HTTP,devido%20a%20conflito%20na%20solicitação..";
                    break;


                ////HTTP 422
                //case InvalidOperationException _:
                //    statusCode = (int)HttpStatusCode.UnprocessableEntity;
                //    errorResponseVm = new ErrorResponseVm("ErroID422", "Mensagem para a exceção especial.");
                //    problemDetails.Extensions["Message"] = "Erro 422, Servidor não pode processar sua solicitação..";
                //    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://kinsta.com/pt/base-de-conhecimento/http-422/";
                //    break;

                //HTTP 500
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponseVm = new ErrorResponseVm("ErroID500", "Ocorreu um erro interno no servidor.");
                    break;
            }

            context.Response.StatusCode = statusCode;
            problemDetails.Status = statusCode;
            problemDetails.Title = GetErrorTitle(statusCode);
            problemDetails.Detail = errorResponseVm.Errors.FirstOrDefault()?.Message;

            var result = JsonConvert.SerializeObject(problemDetails);
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(result);
        }

        private string GetErrorTitle(int statusCode)
        {
            return statusCode switch
            {
                (int)HttpStatusCode.BadRequest => "Requisição inválida",
                (int)HttpStatusCode.Unauthorized => "Não autorizado",
                (int)HttpStatusCode.PaymentRequired => "Pagamento necessário",
                (int)HttpStatusCode.Forbidden => "Acesso proibido",
                (int)HttpStatusCode.NotFound => "Recurso não encontrado",
                (int)HttpStatusCode.Conflict => "Conflito",
                //(int)HttpStatusCode.UnprocessableEntity => "Mensagem para a exceção especial.",
                _ => "Erro interno no servidor",
            };
        }
    }
}