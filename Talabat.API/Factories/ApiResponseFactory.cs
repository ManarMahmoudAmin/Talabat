using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Shared.ErrorModels;

namespace Talabat.API.Factories
{
	public static class ApiResponseFactory
	{
		public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
		{

			var Errors = context.ModelState.Where(M => M.Value.Errors.Any())
			.Select(M => new ValidationError()
			{
				Field = M.Key,
				Errors = M.Value.Errors.Select(E => E.ErrorMessage)
			});
			var response = new ValidationErrorToReturn()
			{
				ValidationErrors = Errors
			};

			return new BadRequestObjectResult(response);
		}

	}
}
