using UnityEngine;

public class CameraRenderImageMaterial : MonoBehaviour
{
	[SerializeField]
	Material _material;

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
        var mat = new Material(_material);
        mat.SetFloat("_TexWidth", src.width);
        mat.SetFloat("_TexHeight", src.height);
        Graphics.Blit(src, dest, mat);
    }
}
