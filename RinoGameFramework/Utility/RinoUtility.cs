using System;

namespace RinoGameFramework.Utility
{
	public class RinoUtility
	{
		public static string NewGuid()
		{
			return Guid.NewGuid().ToString();
		}
	}
}