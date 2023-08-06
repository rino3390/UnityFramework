using RinoGameFramework.Localize.DataScript;
using System;
using VContainer;

namespace RinoGameFramework.Localize
{
	public class LocalizeManager
	{
		[Inject]
		private LocalizeDataSet localizeDataSet;
	
		public event Action<string> OnLanguageChange;
		private string currentLanguage = string.Empty;
		
		public void ChangeLanguage(string language)
		{
			OnLanguageChange?.Invoke(language);
		}
	}
}