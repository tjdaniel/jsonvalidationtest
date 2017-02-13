namespace JsonValidationTest.Serialization
{
  using Newtonsoft.Json.Serialization;

  using System;
  using System.Linq;

  /// <summary>
  /// Resolves key names in a dictionary without camel case specification if attribute is applied
  /// </summary>
  public class CustomCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
	{
		/// <inheritdoc />
		protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
		{
			JsonDictionaryContract contract = base.CreateDictionaryContract(objectType);
			object[] ignoreCamelCase = objectType.GetCustomAttributes(typeof(JsonIgnoreCamelCaseDictionary), true);
			if (ignoreCamelCase.Any())
			{
				contract.DictionaryKeyResolver = propertyName => propertyName;
			}

			return contract;
		}
	}
}