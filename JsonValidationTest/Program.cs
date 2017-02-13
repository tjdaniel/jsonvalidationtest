using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using JsonValidationTest.Representation;
using JsonValidationTest.Serialization;
using JsonValidationTest.Fluent;
using JsonValidationTest.Logger;
using JsonValidationTest.Extensions;

namespace JsonValidationTest
{
  class Program
	{
		// http://stackoverflow.com/questions/15094314/web-api-jsonmediatypeformatter-accepts-invalid-json-and-passes-null-argument-to
		static void Main(string[] args)
    {
      RunValidations();
      RunTestValidation();

      Console.WriteLine("Press any key.");
      Console.ReadKey();
    }

		private static void WriteCaseHeader(ref int nbr)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"*** Case: {++nbr} ***");
			Console.ResetColor();
		}

    private static void RunTestValidation()
    {
      var i = 0;
      WriteCaseHeader(ref i);
      ProcessFile<TestClass>(@"Json\TestClass_all_valid.json")
        .Validate(ValidateTest)
        .ShowResult("Fluent");

      WriteCaseHeader(ref i);
      ProcessFile<TestClass>(@"Json\TestClass_wrong1.json")
        .Validate(ValidateTest)
        .ShowResult("Fluent");
    }

    private static void RunValidations()
    {
			var i = 0;
			WriteCaseHeader(ref i);
      ProcessFile<SnapshotExport>(@"Json\FindSnapshot.json")
        .Validate(ValidateWithAnnotations)
				.ShowResult("DataAnnotation");
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot.json")
				.Validate((s, l) => ValidateFluently(s, l, new SnapshotExportValidator()))
				.ShowResult("Fluent");

			WriteCaseHeader(ref i);
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong.json")
        .Validate(ValidateWithAnnotations)
				.ShowResult("DataAnnotation");
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong.json")
				.Validate((s, l) => ValidateFluently(s, l, new SnapshotExportValidator()))
				.ShowResult("Fluent");

			WriteCaseHeader(ref i);
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong1.json")
        .Validate(ValidateWithAnnotations)
        .Validate(ValidateWithAnnotations, s => s.Csv)
        .Validate(ValidateWithAnnotations, enumFunc: s => s.Parameters)
				.ShowResult("DataAnnotation");
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong1.json")
				.Validate((s, l) => ValidateFluently(s, l, new SnapshotExportValidator()))
				.ShowResult("Fluent");

			WriteCaseHeader(ref i);
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong2.json")
        .Validate(ValidateWithAnnotations, enumFunc: s => s.Parameters)
				.ShowResult("DataAnnotation");
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong2.json")
				.Validate((s, l) => ValidateFluently(s, l, new ParameterValidator()), enumFunc: s => s.Parameters)
				.ShowResult("Fluent");

			WriteCaseHeader(ref i);
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong3.json")
        .Validate(ValidateWithAnnotations, enumFunc: s => s.Parameters)
        .Validate(ValidateWithAnnotations, enumFunc: s => s.Parameters.SelectMany(p => p.SelectedValues))
				.ShowResult("DataAnnotation");
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong3.json")
				.Validate((s, l) => ValidateFluently(s, l, new ParameterValidator()), enumFunc: s => s.Parameters)
				.ShowResult("Fluent");

			WriteCaseHeader(ref i);
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong_format.json")
        .Validate(ValidateWithAnnotations)
        .Validate(ValidateWithAnnotations, s => s.Csv)
				.ShowResult("DataAnnotation");
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong_format.json")
				.Validate((s, l) => ValidateFluently(s, l, new SnapshotExportValidator()))
				.ShowResult("Fluent");

			WriteCaseHeader(ref i);
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong_format1.json")
        .Validate(ValidateWithAnnotations)
        .Validate(ValidateWithAnnotations, s => s.Csv)
				.ShowResult("DataAnnotation");
			ProcessFile<SnapshotExport>(@"Json\FindSnapshot_wrong_format1.json")
				.Validate((s, l) => ValidateFluently(s, l, new SnapshotExportValidator()))
				.ShowResult("Fluent");
		}

		private class ErrorHandler
		{
			public List<string> Messages { get; } = new List<string>();
			public void HandleDeserializationError(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs eventArgs)
			{
				var currentError = eventArgs.ErrorContext.Error.Message;
				Messages.Add(currentError);
				eventArgs.ErrorContext.Handled = true;
			}
		}

		private static T Deserialize<T>(string fileName) where T: class
		{
			// Setup serializer
			var settings = new JsonSerializerSettings();
			settings.Formatting = Formatting.Indented; // Formatting.None
			settings.ContractResolver = new CustomCamelCasePropertyNamesContractResolver();
			//settings.NullValueHandling = NullValueHandling.Ignore;

			var errorHandler = new ErrorHandler();
			settings.Error = errorHandler.HandleDeserializationError;

			JsonSerializer serializer = JsonSerializer.Create(settings);
			T result;
			using (StreamReader file = File.OpenText(fileName))
			{
				result = (T)serializer.Deserialize(file, typeof(T));
			}

			Console.ForegroundColor = ConsoleColor.Red;
			if (errorHandler.Messages.Any())
			{
				Console.WriteLine($"There are {errorHandler.Messages.Count} serialization error(s):");
				foreach (var m in errorHandler.Messages)
				{
					Console.WriteLine($"- {m}");
				}

				Console.WriteLine();
			}

			if (result == null)
			{
				Console.WriteLine("Deserialization did return null");
			}

			Console.ResetColor();
			return result;
		}

		private static Tuple<T, IValidationLogger> ProcessFile<T>(string fileName) where T : class
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine($"Processing Json from: '{fileName}'...");
			Console.ResetColor();

			var subject = Deserialize<T>(fileName);
			return new Tuple<T,IValidationLogger>(subject, new ValidationLogger<T>(subject));
		}

		private static void ValidateWithAnnotations<T>(T subject, IValidationLogger logger) where T : class
		{
			var context = new System.ComponentModel.DataAnnotations.ValidationContext(subject, serviceProvider: null, items: null);
			var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

			var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(subject, context, results, true);

			if (!isValid)
			{
				foreach (var validationResult in results)
				{
					logger.AddMessage(validationResult.ErrorMessage);
				}
			}
		}

		private static void ValidateFluently<T>(T subject, IValidationLogger logger, FluentValidation.AbstractValidator<T> validator)
		{
			FluentValidation.Results.ValidationResult validationResult = validator.Validate(subject);

			if (!validationResult.IsValid)
			{
				foreach (var error in validationResult.Errors)
				{
					logger.AddMessage(error.ErrorMessage);
				}
			}
		}

    private static void ValidateTest(TestClass subject, IValidationLogger logger) 
    {
      var validator = new TestClassValidator();
      FluentValidation.Results.ValidationResult validationResult = validator.Validate(subject);

      if (!validationResult.IsValid)
      {
        foreach (var error in validationResult.Errors)
        {
          logger.AddMessage(error.ErrorMessage);
        }
      }
    }
  }
}
