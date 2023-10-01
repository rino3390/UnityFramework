using Cysharp.Threading.Tasks;
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
			publisher.Publish(new FakeEvent1(id));
		}

		[Button]
		[InfoBox("訂閱 ID > 5 的事件")]
		public void SendFakeEvent2(int id)
		{
			publisher.Publish(new FakeEvent2(id));
		}

		[Button]
		[InfoBox("異步訂閱事件")]
		public async void SendAsyncFakeEvent()
		{
			await publisher.AsyncPublish(new FakeEvent1("1"));
			Debug.Log($"SendAsyncFakeEvent End");
		}

		private void Awake()
		{
			subscriber.Subscribe<FakeEvent1>(OnFakeEvent1, x => x.ID == "1");
			subscriber.Subscribe<FakeEvent2>(OnFakeEvent2, x => x.ID > 5);

			subscriber.SubscribeAsync<FakeEvent1>(AsyncFakeEvent);
			subscriber.SubscribeAsync<FakeEvent1>(AsyncFakeEvent2);
		}

		private void OnFakeEvent1(FakeEvent1 obj)
		{
			Debug.Log($"OnFakeEvent1");
			UniTask.Delay(10000);
		}

		private void OnFakeEvent2(FakeEvent2 obj)
		{
			Debug.Log($"OnFakeEvent2");
		}

		private async UniTask AsyncFakeEvent(FakeEvent1 arg)
		{
			Debug.Log($"AsyncFakeEvent");
			await UniTask.Delay(10000);
			Debug.Log($"AsyncFakeEvent End");
		}

		private UniTask AsyncFakeEvent2(FakeEvent1 arg)
		{
			Debug.Log($"AsyncFakeEvent2");
			return UniTask.CompletedTask;
		}
	}
}