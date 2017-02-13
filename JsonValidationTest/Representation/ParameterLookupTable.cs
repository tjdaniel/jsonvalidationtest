namespace JsonValidationTest.Representation
{
	using System.Collections.Generic;

	public class ParameterLookupTable
	{
	  public List<StringDictionary> Rows { get; set; }

		public string CaptionColumnName { get; set; }

		public string ValueColumnName { get; set; }
	}
}
