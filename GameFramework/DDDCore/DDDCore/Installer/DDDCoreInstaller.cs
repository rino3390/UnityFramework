using GameFramework.DDDCore.Event.CoreEvent;
using MessagePipe;
using Zenject;

namespace GameFramework.DDDCore.Installer
{
	public class DDDCoreInstaller: Installer<DDDCoreInstaller>
	{
		public override void InstallBindings()
		{
			var options = Container.BindMessagePipe();
			Container.BindMessageBroker<IEvent>(options);
			
		}
	}
}