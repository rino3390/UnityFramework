using System;
using System.Collections.Generic;
using UnityEngine;

namespace RinoGameFramework.Localize.DataScript
{
	[CreateAssetMenu(fileName = "LanguageType", menuName = "RinoWeeb/Localize/LanguageType", order = 0)]
	public class LanguageType: ScriptableObject
	{
		public List<string> LanguageName;

		public void Reset()
		{
			LanguageName = new List<string>();
		}
	}
}