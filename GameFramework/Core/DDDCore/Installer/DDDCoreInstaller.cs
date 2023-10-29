using GameFramework.Core.Event;
using MessagePipe;
using Zenject;

namespace GameFramework.Core.Installer
{
	public class DDDCoreInstaller: Installer<DDDCoreInstaller>
	{
		public override void InstallBindings()
		{
			var options = Container.BindMessagePipe();
			Container.BindMessageBroker<IEvent>(options);
			Container.BindInterfacesAndSelfTo<EventBus>().AsSingle();
			Container.Bind<Subscriber>().AsSingle();
			Container.Bind<Publisher>().AsSingle();
		}
	}
}