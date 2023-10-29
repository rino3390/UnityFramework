using GameFramework.Core.Event;

namespace Samples.EventTest
{
	public class FakeEvent2: IEvent
	{
		public int ID { get; }

		public FakeEvent2(int id)
		{
			ID = id;
		}
	}
}