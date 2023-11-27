namespace Order.Application.Exceptions;

public class ApiException: Exception
{
	
	public const int NotFound = 404;
	public const int Unauthorized = 401;
	public const int BadRequest = 400;
	public const int Forbidden = 403;
	public int StatusCode { get; private set; }
	
	public ApiException() { }
	
	public ApiException(string message, int statusCode) : base(message) 
	{
		StatusCode = statusCode;	
	}
	
	public ApiException(string message, System.Exception inner) : base(message, inner) { }
	
	protected ApiException(
		System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context) : base(info, context) { }		
}
