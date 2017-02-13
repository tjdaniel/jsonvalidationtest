namespace JsonValidationTest.Representation
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  /// <summary>
  /// Used as simple dto and reflects the <see cref="Parameter"/> object
  /// </summary>
  public class Parameter : ParameterBase
	{
		/// <summary>
		/// Gets or sets the parameter configuration label.
		/// </summary>
		/// <value>
		/// The parameter configuration label.
		/// </value>
		public string Label { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Parameter"/> is visible.
		/// The default value is true. The state can be changed with the report configuration API Methods CreateReportConfiguration 
		/// and UpdateReportConfigurationParameters or will be defined with the existing <see><cref>ReportParameterVisibility</cref></see> 
		/// definition.
		/// </summary>
		/// <value>
		///   <c>true</c> if visible; otherwise, <c>false</c>.
		/// </value>
		public bool Visible { get; set; }


		/// <summary>
		/// Gets or sets a value indicating whether this parameter requires values.
		/// </summary>
		/// <value>
		/// <c>true</c> if this parameter requires values; otherwise, <c>false</c>.
		/// </value>
		public bool IsRequired { get; set; }

		/// <summary>
		/// Gets or sets a list of parameter dependencies (parameter Id) whose values are used to execute lookup expressions. 
		/// </summary>
		/// <value>
		/// The parameter dependencies.
		/// </value>
		public IList<int> DependsOn { get; set; }


		/// <summary>
		/// Gets or sets the error message.
		/// </summary>
		/// <value>
		/// The error message.
		/// </value>
		public string ErrorMessage { get; set; }

		/// <summary>
		/// Gets or sets the available lookup table.
		/// </summary>
		/// <value>
		/// The available lookup table.
		/// </value>
		public ParameterLookupTable LookupTable { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has a lookup table.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance has a lookup table; otherwise, <c>false</c>.
		/// </value>
		public bool HasLookupTable { get; set; }

		/// <summary>
		/// Gets or sets the order in which the parameter should be presented to the user
		/// </summary>
		public decimal Order { get; set; }

		/// <summary>
		///  Gets or sets valid values for the parameter
		/// </summary>
		public IList<ParameterValue> ValidValues { get; set; }

		/// <summary>
		/// Gets or sets the selected values.
		/// </summary>
		[Required]
		public IList<ParameterValue> SelectedValues { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether parameter is read only
		/// </summary>
		public bool IsReadOnly { get; set; }
	}
}
