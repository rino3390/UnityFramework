using RinoGameFramework.Localize.DataScript;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RinoGameFramework.Localize.Container
{
	public class LocalizeContainer: LifetimeScope
	{
		[SerializeField]
		private LocalizeDataSet localizeDataSet;

		[SerializeField]
		private LanguageList languageList;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(localizeDataSet);
			builder.RegisterInstance(languageList);

			builder.Register<LocalizeManager>(Lifetime.Singleton);
		}

		protected override void Awake()
		{
			base.Awake();
			var localizeObj = FindObjectsByType<LocalizeTMP>(FindObjectsSortMode.None);

			foreach (var localize in localizeObj)
			{
				Container.Inject(localize);
			}
		}
	}
}