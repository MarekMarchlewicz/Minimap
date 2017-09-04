using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MinimapCamera : MonoBehaviour 
{
	[SerializeField] private Transform targetToFollow;

    private RenderTexture targetRenderTexture;
    private RenderTexture targetDepthTexture;

    public RenderTexture RenderTex { get { return targetRenderTexture; } }
    public RenderTexture DepthTex { get { return targetDepthTexture; } }

    private void Awake()
	{
        targetRenderTexture = new RenderTexture(256, 256, 0, RenderTextureFormat.Default);
        targetDepthTexture = new RenderTexture(256, 256, 24, RenderTextureFormat.Depth);
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
        GetComponent<Camera>().SetTargetBuffers(targetRenderTexture.colorBuffer, targetDepthTexture.depthBuffer);
    }
    
	private void LateUpdate()
	{
		Vector3 newPosition = transform.position;
		newPosition.x = targetToFollow.position.x;
		newPosition.z = targetToFollow.position.z;

        Vector3 newRotation = targetToFollow.rotation.eulerAngles;
        newRotation.x = 0f;
        newRotation.z = 0f;
        Debug.Log(newRotation.y);

		transform.position = newPosition;

        transform.rotation = Quaternion.Euler(newRotation) * Quaternion.Euler(Vector3.right * 90);
	}
}
