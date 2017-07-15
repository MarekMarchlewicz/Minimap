using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class MinimapUI : MonoBehaviour 
{
	[SerializeField] private MinimapCamera minimapCamera;

	[SerializeField] private float maxHeight, minHeight;

	private RawImage m_RawImage;

	private Material m_Material;

	private int minId, maxId;

	private void Awake()
	{
		m_RawImage = GetComponent<RawImage> ();
		m_Material = GetComponent<RawImage> ().material;

		minimapCamera.OnUpdatedTexture += OnUpdateTexture;

		maxId = Shader.PropertyToID ("_MaxHeight");
		minId = Shader.PropertyToID ("_MinHeight");
	}

	private void OnUpdateTexture(Texture updatedTexture)
	{
		m_RawImage.texture = updatedTexture;

		m_Material.SetFloat (maxId, maxHeight);
		m_Material.SetFloat (minId, minHeight);
	}
}
