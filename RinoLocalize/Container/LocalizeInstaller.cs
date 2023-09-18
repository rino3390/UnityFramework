using RinoLocalize.RunTime;
using Zenject;

namespace RinoLocalize.Container
{
	public class LocalizeInstaller: Installer<LocalizeInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<LocalizeManager>().AsSingle().NonLazy();
		}
	}
}