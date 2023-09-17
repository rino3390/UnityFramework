using RinoLocalize.DataScript;
using System;

namespace RinoLocalize.RunTime
{
	public class LocalizeManager
	{
		public event Action<string> OnLanguageChange;

		public string CurrentLanguage;
		private readonly LanguageList languageList;

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