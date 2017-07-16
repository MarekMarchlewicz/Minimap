using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MinimapCamera : MonoBehaviour 
{
	[SerializeField] private Transform targetToFollow;

	public System.Action<RenderTexture, RenderTexture> OnUpdatedTexture;

	private void Awake()
	{
		GetComponent<Camera> ().depthTextureMode = DepthTextureMode.Depth;
	}

	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (OnUpdatedTexture != null) 
		{
			OnUpdatedTexture (source, destination);
		}
	}

	private void LateUpdate()
	{
		Vector3 newPosition = transform.position;

		newPosition.x = targetToFollow.position.x;
		newPosition.z = targetToFollow.position.z;

		transform.position = newPosition;
	}
}
