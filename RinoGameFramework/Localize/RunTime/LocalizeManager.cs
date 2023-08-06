using RinoGameFramework.Localize.DataScript;
using System;
using VContainer;

namespace RinoGameFramework.Localize
{
	public class LocalizeManager
	{
		public event Action<string> OnLanguageChange;

		public string CurrentLanguage;
		private readonly LanguageList languageList;

		[Inject]
		private LocalizeDataSet localizeDataSet;

		public LocalizeManager(LanguageList languageList,LocalizeDataSet localizeDataSet)
		{
			this.languageList = languageList;
			this.localizeDataSet = localizeDataSet;
			CurrentLanguage = languageList.DefaultLanguage;
		}

		public void ChangeLanguage(string language)
		{
			OnLanguageChange?.Invoke(language);
		}
	}
}