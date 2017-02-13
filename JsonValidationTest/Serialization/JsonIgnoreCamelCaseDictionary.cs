namespace JsonValidationTest.Serialization
{
	using System;

	/// <summary>
	/// Ignore camel case strategy naming of the dictionary property keys
	/// </summary>
	 [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
	public class JsonIgnoreCamelCaseDictionary : Attribute
	{
	}
}