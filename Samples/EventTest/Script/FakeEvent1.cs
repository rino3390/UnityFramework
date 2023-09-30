using GameFramework.Core.Event;

namespace Samples.EventTest
{
	public class FakeEvent1: IEvent
	{
		public string ID { get; }
		public FakeEvent1(string id)
		{
			ID = id;
		}
	}
}