using System;

namespace RinoGameFramework.Utility.Editor.Validate
{
	public class UniqueListAttribute: Attribute
	{
		public string ErrorMessage { get; }

		public UniqueListAttribute(string errorMessage = "清單值重複")
		{
			ErrorMessage = errorMessage;
		}
	}
}