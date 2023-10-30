using GameFramework.RinoUtility.Editor;
using JetBrains.Annotations;
using RinoLocalize.DataScript;
using System.Collections;
using System.Linq;

namespace RinoLocalize.Editor
{
	public static class LocalizeDrawer
	{
		[UsedImplicitly]
		public static IEnumerable LocalizeStingIdDropDown()
		{
			var localizeDataSet = RinoEditorUtility.FindAsset<LocalizeDataSet>();
			return localizeDataSet == null ? null : localizeDataSet.LocalizeStringDropDown();
		}
		
		public static bool HasLocalizeData(string id)
		{
			var localizeDataSet = RinoEditorUtility.FindAsset<LocalizeDataSet>();
			return localizeDataSet.LocalizeStringDatas.Any(x => x.Id == id);
		}
	}
}