namespace RinoGameFramework.Attribute
{
	public class UniqueListAttribute: System.Attribute
	{
		public string ErrorMessage { get; }

		public UniqueListAttribute(string errorMessage = "清單值重複")
		{
			ErrorMessage = errorMessage;
		}
	}
}