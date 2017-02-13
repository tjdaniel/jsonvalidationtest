using System;
using System.Collections.Generic;

namespace JsonValidationTest.Representation
{
  public enum TestOption
	{
		Option1,
		Option2,
		Option3,
		Option4
	}

	public class TestClass
	{
		public string NonNullableText { get; set; }

    public string NotEmptyText { get; set; }

    public string MinLenText { get; set; }

    public bool MustBeValidOption { get; set; }

    public TestOption ValidOption { get; set; }

    public TestOption Option { get; set; }

    public List<string> TextItems { get; set; }

		public DateTime? UtcDate { get; set; }
	}
}
