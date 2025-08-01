using System.Reflection;
using Microsoft.Build.Framework;

namespace SharpIDE.Application.Features.Logging;

public class InternalTerminalLoggerFactory
{
	public static ILogger CreateLogger()
	{
		var logger = CreateLogger("FORCECONSOLECOLOR", LoggerVerbosity.Minimal);
		return logger;
	}

	public static ILogger CreateLogger(string parameters, LoggerVerbosity loggerVerbosity)
	{
		var type = Type.GetType("Microsoft.Build.Logging.TerminalLogger, Microsoft.Build");

		if (type == null) throw new Exception("TerminalLogger type not found");

		var method = type.GetMethod(
			"CreateTerminalOrConsoleLogger",
			BindingFlags.NonPublic | BindingFlags.Static);

		if (method == null) throw new Exception("CreateTerminalOrConsoleLogger method not found");

		string[]? args = [];
		bool supportsAnsi = true;
		bool outputIsScreen = true;
		uint? originalConsoleMode = 0x0007;

		object? logger = method.Invoke(
			obj: null,
			parameters: [args, supportsAnsi, outputIsScreen, originalConsoleMode]);

		var castLogger = (ILogger)logger!;
		castLogger.Parameters = parameters;
		castLogger.Verbosity = loggerVerbosity;
		return castLogger;
	}
}
