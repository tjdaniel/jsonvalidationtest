using System.ComponentModel.DataAnnotations;

namespace JsonValidationTest.Representation
{
	public class ParameterValue
	{
		public string Label { get; set; }

		[Required]
		public object Value { get; set; }
	}
}
