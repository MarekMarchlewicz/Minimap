using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MinimapCamera : MonoBehaviour 
{
	[SerializeField] private Transform targetToFollow;

	public System.Action<Texture> OnUpdatedTexture;

	private void Awake()
	{
		GetComponent<Camera> ().depthTextureMode = DepthTextureMode.Depth;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (OnUpdatedTexture != null) 
		{
			OnUpdatedTexture (source);
		}
	}

	private void LateUpdate()
	{
		Vector3 newPosition = transform.position;

		newPosition.x = targetToFollow.position.x;
		newPosition.y = targetToFollow.position.y;

		transform.position = newPosition;
	}
}
