using RinoGameFramework.Localize;
using RinoGameFramework.RinoUtility;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RinoGameFramework.GameManager.DataScript.Language
{
	[CreateAssetMenu(fileName = "LanguageDataOverview", menuName = "Data/Language")]
	public class LanguageDataOverview: SerializedScriptableObject
	{
		[SerializeField]
		[HideLabel, Title("新增Language Id")]
		[PropertyOrder(-1)]
		private LanguageData Create = new LanguageData();

		[Button, PropertyOrder(-1)]
		[PropertySpace(10, 10)]
		public void CreateLanguage()
		{
			Create.Id = GUID.NewGuid();

			if(string.IsNullOrEmpty(Create.LanguageId))
			{
				return;
			}

			Languages.Add(Create);
			Create = new LanguageData();
		}

		[SerializeField]
		[ListDrawerSettings(HideAddButton = true,ListElementLabelName = "LanguageId",NumberOfItemsPerPage = 20)]
		[DisableContextMenu,Searchable]
		private List<LanguageData> Languages = new List<LanguageData>();

		public LanguageStringValue GetString(string Id)
		{
			var data = Languages.Find(x=> x.Id == Id);

			if(data == null)
			{
				throw new MissingReferenceException($"LanguageId{Id} 不存在");
			}

			return data.Language;
		}

	#if UNITY_EDITOR
		public List<(string id,string root)> LanguageIdList => Languages.Select(languageData => (languageData.Id,ArgRoot(languageData) + languageData.LanguageId)).ToList();

		public IEnumerable LanguageDropDown()
		{
			return Languages.Select(languageData => new ValueDropdownItem(ArgRoot(languageData) + languageData.LanguageId, languageData.Id));
		}

		private static string ArgRoot(LanguageData language)
		{
			return string.IsNullOrEmpty(language.Root) ? "" : language.Root + "/";
		}

	#endif
		
	}
}