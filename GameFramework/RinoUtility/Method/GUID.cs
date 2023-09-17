using System;

namespace GameFramework.RinoUtility.Method
{
	public class GUID
	{
		public static string NewGuid()
		{
			return Guid.NewGuid().ToString();
		}
	}
}