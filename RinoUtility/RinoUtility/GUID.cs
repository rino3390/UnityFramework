using System;

namespace RinoUtility
{
	public class GUID
	{
		public static string NewGuid()
		{
			return Guid.NewGuid().ToString();
		}
	}
}