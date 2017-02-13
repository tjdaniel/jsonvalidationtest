using System.ComponentModel.DataAnnotations;

namespace JsonValidationTest.Representation
{
	public class SnapshotCsvExport
	{
		#region Public Properties

		/// <summary>
		/// The name of the culture used to convert values for CSV files.
		/// The culture name in the format "[languagecode2]-[country/regioncode2]",
		/// where [languagecode2] is a lowercase two-letter code derived from ISO 639-1
		/// and [country/regioncode2] is an uppercase two-letter code derived from ISO 3166.
		/// For more information on the culture name go to
		/// http://msdn.microsoft.com/en-us/goglobal/bb896001.aspx
		/// </summary>
		[Required]
		public string Culture { get; set; }

		/// <summary>
		/// The name for the Encoding used to save CSV files.
		/// For more information on the encoding name, go to
		/// http://msdn.microsoft.com/en-us/library/system.text.encodinginfo.name.aspx
		/// </summary>
		public string Encoding { get; set; }

		/// <summary>
		/// Delimiter used to separate field values in CSV file.
		/// Using of Quote (") as field separator is not allowed.
		/// </summary>
		public char FieldDelimiter { get; set; }

		/// <summary>
		/// Specifies if the enclosure used to enclose field values is mandatory.
		/// </summary>
		public bool UseEnclosure { get; set; }

		// To get [Required] to work on value types, they must be nullable

		/// <summary>
		/// Flag whether the index file will be generated.
		/// </summary>
		[Required]
		public bool? WriteIndexFile { get; set; }

		#endregion
	}
}