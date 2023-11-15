using FluentValidation;
using Identity.Business.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Identity.Business.Middlewares;

public class ExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	private const int DUPLICATE = -2146232060;
	
	public ExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}
	
	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next.Invoke(context);
		}
		catch (ApiException ex)
		{
			await HandleApiExceptionMessageAsync(context, ex);
		}
		catch (DbUpdateException ex)
		{
			await HandleDbExceptionAsync(context, ex);
		}
		catch (Exception)
		{
			await HandleInternalServerErrorAsync(context);
		}
	}

	private async Task HandleDbExceptionAsync(HttpContext context, DbUpdateException ex)
	{
		string result;
		if (ex.InnerException!.HResult == DUPLICATE)
		{
			result = JsonConvert.SerializeObject(new
			{
				StatusCode = ApiException.BadRequest,
				ErrorMessage = "one or more fields are already reserved"
			});
			context.Response.StatusCode = ApiException.BadRequest;
		} 
		else 
		{
			result = JsonConvert.SerializeObject(new
			{
				StatusCode = 500,
				ErrorMessage = "Internal Server Error"
			});
			context.Response.StatusCode = 500;
		}

		context.Response.ContentType = "application/json";
		await context.Response.WriteAsync(result);

	}

	private static async Task HandleApiExceptionMessageAsync(HttpContext context, ApiException exception) 
	{				
		var result = JsonConvert.SerializeObject(new  
		{  
			StatusCode = exception.StatusCode,  
			ErrorMessage = exception.Message  
		});  
		context.Response.ContentType = "application/json";  
		context.Response.StatusCode = exception.StatusCode;  
		await context.Response.WriteAsync(result);  
	}
	
	private static async Task HandleInternalServerErrorAsync(HttpContext context)
	{
		var result = JsonConvert.SerializeObject(new
		{
			StatusCode = 500,
			ErrorMessage = "Internal Server Error"
		});
		context.Response.ContentType = "application/json";  
		context.Response.StatusCode = 500;  
		await context.Response.WriteAsync(result);  
	}	
}