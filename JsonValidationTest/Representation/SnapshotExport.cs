namespace JsonValidationTest.Representation
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using Newtonsoft.Json;

  public class SnapshotExport
	{
		#region Public Properties

		/// <summary>
		/// Options how to export CSV files.
		/// </summary>
		[Required]
		public SnapshotCsvExport Csv { get; set; }

		/// <summary>
		/// Target path where the CSV files will be written.
		/// </summary>
		//[JsonProperty(Required = Required.Always)]
		[Required]
		public string OutputPath { get; set; }


		/// <summary>
		/// The parameters used in the report execution.
		/// </summary>
		[Required]
		public IEnumerable<Parameter> Parameters { get; set; }

		#endregion
	}
}