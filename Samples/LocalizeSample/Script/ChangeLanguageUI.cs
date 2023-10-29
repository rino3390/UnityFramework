using RinoLocalize.RunTime;
using UnityEngine;
using Zenject;

namespace Samples.LocalizeSample.Script
{
	public class ChangeLanguageUI: MonoBehaviour
	{
		[Inject]
		private LocalizeManager localizeManager;
		
		public void ChangeLanguage(string language)
		{
			localizeManager.ChangeLanguage(language);
		}
	}
}