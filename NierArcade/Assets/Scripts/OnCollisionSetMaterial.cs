using UnityEngine;

public class OnCollisionSetMaterial : MonoBehaviour
{
	[SerializeField]
	ArcadeGameObjectTags _tag;

	[SerializeField]
	GameObject _gameObjectWithRenderer;

	[SerializeField]
	Material _material;

	[SerializeField]
	float _duration;

	MeshRenderer _meshRenderer;

	SkinnedMeshRenderer _skinMeshRenderer;

	RendererType _rendererType;

	Material[] _enable, _disable;

	bool _fallback;

	float _seconds;

	void Awake()
	{
		_meshRenderer = _gameObjectWithRenderer.GetComponent<MeshRenderer>();
		if (_meshRenderer != null)
		{
			_rendererType = RendererType.Mesh;

			_enable = new Material[2] { _meshRenderer.material, _material };
			_disable = new Material[1] { _meshRenderer.material };
		}
		else
		{
			_skinMeshRenderer = _gameObjectWithRenderer.GetComponent<SkinnedMeshRenderer>();
			if (_skinMeshRenderer != null)
			{
				_rendererType = RendererType.SkinMesh;
				_enable = new Material[2] { _skinMeshRenderer.material, _material };
				_disable = new Material[1] { _skinMeshRenderer.material };
			}
		}
	}

	void Update()
	{
		if (_fallback)
		{
			if (_seconds >= _duration)
			{
				_seconds = 0f;
				_fallback = false;

				switch (_rendererType)
				{
					case RendererType.Mesh:
						_meshRenderer.materials = _disable;
						break;

					case RendererType.SkinMesh:
						_skinMeshRenderer.materials = _disable;
						break;
				}
			}
			else _seconds += Time.deltaTime;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if ( ! collision.gameObject.CompareTag(_tag.ToString()))
			return;

		switch (_rendererType)
		{
			case RendererType.Mesh:
				_meshRenderer.materials = _enable;
				break;

			case RendererType.SkinMesh:
				_skinMeshRenderer.materials = _enable;
				break;
		}

		_fallback = true;
	}

	enum RendererType
	{
		None,
		Mesh,
		SkinMesh
	}
}
