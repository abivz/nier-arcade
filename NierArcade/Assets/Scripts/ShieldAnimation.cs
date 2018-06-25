using UnityEngine;

public class ShieldAnimation : MonoBehaviour
{
	[SerializeField]
	float _speed;

	Material _shieldMaterial;

	float _offset = 0f;

	void Awake()
	{
		_shieldMaterial = GetComponent<MeshRenderer>().material;
	}

	void Update()
	{
		_offset += _speed * Time.deltaTime;
		if (_offset < 0f)
			_offset = 1f;
		else if (_offset > 1f)
			_offset = 0f;

		_shieldMaterial.SetTextureOffset("_MainTex", new Vector2(0f, _offset));
	}
}
