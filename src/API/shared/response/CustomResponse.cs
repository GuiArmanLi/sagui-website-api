using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public record CustomResponse() : IActionResult
{
    public HttpStatusCode StatusCode { get; private set; }
    public string? Message { get; private set; }
    public object? Content { get; private set; }

    public CustomResponse(HttpStatusCode httpStatus, object? content, string? message = "Default message")
        : this()
    {
        StatusCode = httpStatus;
        Content = content;
        Message = message;
    }

    public CustomResponse(HttpStatusCode httpStatus, string message = "Default message") : this(httpStatus, null, message)
    {
    }

    public CustomResponse(HttpStatusCode httpStatus) : this(httpStatus, "Default message")
    {
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var httpResponse = new CustomResponse(StatusCode, Content, Message!);

        var response = context.HttpContext.Response;
        response.StatusCode = (int)httpResponse.StatusCode;
        response.ContentType = "application/json";

        await response.WriteAsync(
            JsonConvert.SerializeObject(httpResponse)
        );
    }
}