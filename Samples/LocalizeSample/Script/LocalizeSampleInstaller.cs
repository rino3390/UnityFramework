using RinoLocalize.Container;
using RinoLocalize.DataScript;
using UnityEngine;
using Zenject;

namespace Samples.LocalizeSample.Script
{
	public class LocalizeSampleInstaller: MonoInstaller<LocalizeSampleInstaller>
	{
		[SerializeField]
		private LocalizeDataSet localizeDataSet;

		[SerializeField]
		private LanguageList languageList;
		
		public override void InstallBindings()
		{
			LocalizeInstaller.Install(Container);
			Container.BindInstance(localizeDataSet);
			Container.BindInstance(languageList);
		}
	}
}