using System.Text.RegularExpressions;

namespace Identity.Presentation;

public static class Startup
{
	public static string GetConfiguredConnectionString(this IConfiguration configuration, string variant)
	{
		var connectionString = configuration.GetConnectionString(variant);	
		
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new ArgumentException("There is no such variant of connection string");
		}		
				
		var matches = Regex.Matches(connectionString, @"\$\{\w+\}");
		
		foreach (Match match in matches)
		{
			var arg = match.Value;
			var envVariable = Environment.GetEnvironmentVariable(arg[2..^1]);
			
			if (envVariable is null)
			{
				throw new ArgumentException($"There is no environment variable {arg[2..^1]}");
			}

			connectionString = connectionString.Replace(arg, envVariable);
		}	

		return connectionString;
	}
	
	public static string GetConnectionStringVariant(this string[] args)
	{
		if (args.Length >= 1)
			return args[0];

		return "Default";
	}
}