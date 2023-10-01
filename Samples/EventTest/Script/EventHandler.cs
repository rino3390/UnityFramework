using GameFramework.Core.Event;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Samples.EventTest
{
	public class EventHandler: MonoBehaviour
	{
		[Inject]
		private Subscriber subscriber;

		[Inject]
		private Publisher publisher;
		
		[Button]
		[InfoBox("訂閱 ID = 1 的事件")]
		public void SendFakeEvent1(string id)
		{
			for(int i = 0; i < 1000; i++)
			{
				publisher.Publish(new FakeEvent1(id));
			}
		}

		[Button]
		[InfoBox("訂閱 ID > 5 的事件")]
		public void SendFakeEvent2(int id)
		{
			publisher.Publish(new FakeEvent2(id));
		}

		private void Awake()
		{
			subscriber.Subscribe<FakeEvent1>(OnFakeEvent1, x => x.ID == "1");
			subscriber.Subscribe<FakeEvent2>(OnFakeEvent2, x=> x.ID > 5);
		}

		private void OnFakeEvent1(FakeEvent1 obj)
		{
			Debug.Log($"OnFakeEvent1");	
		}

		private void OnFakeEvent2(FakeEvent2 obj)
		{
			Debug.Log($"OnFakeEvent2");
		}
	}
}