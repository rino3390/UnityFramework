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
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(localizeDataSet);
			builder.Register<LocalizeManager>(Lifetime.Singleton);
		}
	}
}