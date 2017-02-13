using JsonValidationTest.Logger;
using System;
using System.Collections.Generic;

namespace JsonValidationTest.Extensions
{
	public static class ValidationExtensions
	{
		public static Tuple<T, IValidationLogger> Validate<T>(this Tuple<T, IValidationLogger> source, Action<T, IValidationLogger> validate) where T : class
		{
			if (source.Item1 != null)
			{
				validate(source.Item1, source.Item2);
			}

			return source;
		}

		public static Tuple<T, IValidationLogger> Validate<T, T1>(this Tuple<T, IValidationLogger> source, Action<T1, IValidationLogger> validate, Func<T, T1> func) where T : class where T1 : class
		{
			if (source.Item1 != null)
			{
				var result = func(source.Item1);

				if (result != null)
				{
					validate(result, source.Item2);
				}
			}

			return source;
		}

		public static Tuple<T, IValidationLogger> Validate<T, T1>(this Tuple<T, IValidationLogger> source, Action<T1, IValidationLogger> validate, Func<T, IEnumerable<T1>> enumFunc) where T : class where T1 : class
		{
			if (source.Item1 != null)
			{
				var result = enumFunc(source.Item1);

				foreach (var item in result)
				{
					validate(item, source.Item2);
				}
			}

			return source;
		}

		public static Tuple<T, IValidationLogger> ShowResult<T>(this Tuple<T, IValidationLogger> source, string validationType)
		{
			if (source != null)
			{
				source.Item2.Write(validationType);
			}

			return source;
		}
	}
}
