using System;

namespace RinoGameFramework.Utility
{
	public class GUID
	{
		public static string NewGuid()
		{
			return Guid.NewGuid().ToString();
		}
	}
}