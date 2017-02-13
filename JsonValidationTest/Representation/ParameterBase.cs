namespace JsonValidationTest.Representation
{
	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Used as simple dto and reflects the <see cref="ParameterBase"/> object
	/// </summary>
	public class ParameterBase
	{
		/// <summary>
		/// Gets or sets the unique parameter identifier.
		/// </summary>
		/// <value>
		/// The unique parameter identifier.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the unique parameter name.
		/// </summary>
		/// <value>
		/// The unique parameter name.
		/// </value>
		[Required]
		public string Name { get; set; }
	
		/// <summary>
		/// Gets or sets a value indicating whether the parameter is a multivalued parameter.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is multivalued; otherwise, <c>false</c>.
		/// </value>
		public bool IsMultival { get; set; }

		/// <summary>
		/// Gets or sets the parameter values (CLR) type FullName to the ValCalcTypeSign.
		/// </summary>
		/// <value>
		/// The parameter values type.
		/// </value>
		public string TypeName { get; set; }
	}
}
