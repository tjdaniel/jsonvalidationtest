using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonValidationTest.Logger
{
	public class ValidationLogger<T>: IValidationLogger where T : class
	{
		private readonly List<string> _messages = new List<string>();

		private readonly T _subject;

		public ValidationLogger(T subject)
		{
			_subject = subject;
		}

		public void AddMessage(string message)
		{
			_messages.Add(message);
		}

		public void Write(string validationType)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine($"[{validationType}]");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine($"Validating instance of type: '{typeof(T).Name}'...");

			if (_messages.Any())
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"There are {_messages.Count} validation error(s):");
				foreach (var message in _messages)
				{
					Console.WriteLine($"- {message}");
				}
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"No validation error.");
			}

			Console.ResetColor();
			Console.WriteLine();
		}
	}
}
