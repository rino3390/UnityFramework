using GameFramework.Core.Installer;
using Zenject;

namespace Samples.EventTest
{
	public class EventTestInstaller: MonoInstaller
	{
		public override void InstallBindings()
		{
			DDDCoreInstaller.Install(Container);
		}
	}
}