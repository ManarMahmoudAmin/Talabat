using System.Net;
using System.Text.Json;
using Talabat.Core.Exceptions;
using Talabat.Core.Shared.ErrorModels;

namespace Talabat.API.CustomMiddlewares
{
	public class CustomExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, "Something went wrong");

				///set status code for response
				//context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.StatusCode = ex switch
				{
					 NotFoundException => StatusCodes.Status404NotFound, 
					 UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
					 BadRequestException => StatusCodes.Status400BadRequest,
					_ => StatusCodes.Status500InternalServerError
				};

				///set content type for response
				//context.Response.ContentType = "application/json";

				//create reponse object
				var response = new ErrorToReturn()
				{
					StatusCode = context.Response.StatusCode,
					ErrorMessage = ex.Message
				};

				///return object as json
				
				//await context.Response.WriteAsync(JsonSerializer.Serialize(response));
				await context.Response.WriteAsJsonAsync(response);
			}

		}
	}
}
