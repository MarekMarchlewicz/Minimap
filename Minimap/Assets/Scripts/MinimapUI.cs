using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class MinimapUI : MonoBehaviour 
{
	[SerializeField] private MinimapCamera minimapCamera;

	[SerializeField] private Terrain terrain;

	private float maxHeight, minHeight;

	private RawImage m_RawImage;

	private Material m_Material;

	private Camera m_Camera;

	private int minId, maxId, camDistId, camPosId;

	private void Awake()
	{
		m_RawImage = GetComponent<RawImage> ();
		m_Material = GetComponent<RawImage> ().material;
		m_Camera = minimapCamera.GetComponent<Camera> ();

		minimapCamera.OnUpdatedTexture += OnUpdateTexture;

		maxId = Shader.PropertyToID ("_MaxHeight");
		minId = Shader.PropertyToID ("_MinHeight");
		camDistId = Shader.PropertyToID ("_CameraDistance");
		camPosId = Shader.PropertyToID ("_WSCameraPos");

		SetMinMaxPoints ();
	}

	private void OnUpdateTexture(RenderTexture source, RenderTexture destination)
	{
		m_Material.mainTexture = source;
		m_Material.SetFloat (camDistId, m_Camera.farClipPlane - m_Camera.nearClipPlane);
		m_Material.SetVector (camPosId, m_Camera.transform.position);

		//RaycastCornerBlit (source, destination, m_Material);

		m_RawImage.texture = destination;
	}

	private void SetMinMaxPoints()
	{
		minHeight = float.MaxValue;
		maxHeight = float.MinValue;

		float[,] heights = terrain.terrainData.GetHeights (0, 0, terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);

		for (int x = 0; x < heights.GetLength(0); x++) 
		{
			for (int y = 0; y < heights.GetLength (1); y++) 
			{
				float height = heights [x, y];

				if (height > maxHeight)
					maxHeight = height;
				if (height < minHeight)
					minHeight = height;
			}
		}

		minHeight *= 600f;
		maxHeight *= 600f;

		m_Material.SetFloat (maxId, maxHeight);
		m_Material.SetFloat (minId, minHeight);
	}
}
