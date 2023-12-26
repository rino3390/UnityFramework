using System.Text.RegularExpressions;

namespace GameFramework.RinoUtility.Misc
{
	public class RegexChecking
	{
		public static bool OnlyEnglishAndNum(string checkString)
		{
			return Regex.IsMatch(checkString, @"^[a-zA-Z0-9-_ ]+$");
		}
	}
}