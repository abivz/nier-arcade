using UnityEngine;

public class FollowToTag : CachedMonoBehaviour
{
	[SerializeField]
	GameObjectTag _tag;

    [SerializeField]
    Vector3 _offsetFromTargetGameObject;

	[SerializeField]
	FollowType _followType;

	[SerializeField]
	float _timeMultiplier;

    Transform _targetTransform;

    void Awake()
    {
        cachedTransform.position = _offsetFromTargetGameObject;
    }

    void LateUpdate()
	{
        if (_targetTransform == null)
        {
            var go = GameObject.FindGameObjectWithTag(_tag.ToString());
            if (go != null)
                _targetTransform = go.GetComponent<Transform>();
            
            return;
        }

		var playerPosition = _targetTransform.position;

		switch (_followType)
		{
			case FollowType.None:
				cachedTransform.position = playerPosition + _offsetFromTargetGameObject;
			break;

			case FollowType.Lerp:
				cachedTransform.position = Vector3.Lerp(cachedTransform.position,
														playerPosition + _offsetFromTargetGameObject,
														_timeMultiplier * Time.deltaTime);
			break;

			case FollowType.SmoothStep:
				cachedTransform.position = SmoothStep(cachedTransform.position,
														playerPosition + _offsetFromTargetGameObject,
														_timeMultiplier * Time.deltaTime);
			break;
		}
	}
	
	Vector3 SmoothStep(Vector3 from, Vector3 to, float t)
	{
		var x = Mathf.SmoothStep(from.x, to.x, t);
		var y = Mathf.SmoothStep(from.y, to.y, t);
		var z = Mathf.SmoothStep(from.z, to.z, t);
				
		return new Vector3(x, y, z);
	}

	enum FollowType
	{
		None,
		Lerp,
		SmoothStep
	}
}
