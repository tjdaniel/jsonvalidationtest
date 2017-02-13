namespace JsonValidationTest.Logger
{
	public interface IValidationLogger
	{
		void AddMessage(string message);

		void Write(string validationType);
	}
}
