using RinoLocalize.DataScript;
using System;
using System.Linq;
using UnityEngine;

namespace RinoLocalize.RunTime
{
	public class LocalizeManager
	{
		public event Action<string> OnLanguageChange;

		public string CurrentLanguage { get; private set; }

		private readonly LanguageList languageList;
		private readonly LocalizeDataSet localizeDataSet;

		public LocalizeManager(LanguageList languageList, LocalizeDataSet localizeDataSet)
		{
			this.languageList = languageList;
			this.localizeDataSet = localizeDataSet;
			CurrentLanguage = languageList.DefaultLanguage;
		}

		public void ChangeLanguage(string language)
		{
			if(languageList.LanguageName.All(l=> l.Language != language))
			{
				Debug.LogError($"Language {language} not found");
				return;
			}
			CurrentLanguage = language;
			OnLanguageChange?.Invoke(language);
		}
		
		public string GetLocalizeString(string id)
		{
			return localizeDataSet.GetString(id).Find(x => x.Language == CurrentLanguage).Value;
		}

		public Sprite GetLocalizeImage(string id)
		{
			return localizeDataSet.GetImage(id).Find(x => x.Language == CurrentLanguage).Value;
		}

		public AudioClip GetLocalizeAudio(string id)
		{
			return localizeDataSet.GetAudio(id).Find(x => x.Language == CurrentLanguage).Value;
		}
	}
}