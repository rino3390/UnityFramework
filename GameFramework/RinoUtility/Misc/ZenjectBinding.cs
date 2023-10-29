using UnityEngine;
using Zenject;

namespace GameFramework.RinoUtility.Misc
{
	public class ZenjectBinding
	{
		
		public static void BindWithCurrentContainer(GameObject gameObject)
		{
			var diContainer = ProjectContext.Instance.Container.Resolve<SceneContextRegistry>()
												  .GetContainerForScene(gameObject.scene);
			diContainer.InjectGameObject(gameObject);
		}
	}
}