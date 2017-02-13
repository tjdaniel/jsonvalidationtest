namespace JsonValidationTest.Representation
{
  using System.Collections.Generic;
  using Serialization;

  /// <summary>
  /// String dictionarry without camel case serialization specification
  /// </summary>
  [JsonIgnoreCamelCaseDictionary]
	public class StringDictionary : Dictionary<string, string>
	{
	}
}