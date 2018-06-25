using UnityEngine;

public class CameraRenderImageMaterial : MonoBehaviour
{
	[SerializeField]
	Material _material;

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		_material.SetFloat("_TexWidth", src.width);
		_material.SetFloat("_TexHeight", src.height);
        Graphics.Blit(src, dest, _material);
    }
}
