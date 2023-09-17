using System;

namespace GameFramework.RinoUtility.Misc
{
	public class GUID
	{
		public static string NewGuid()
		{
			return Guid.NewGuid().ToString();
		}
	}
}