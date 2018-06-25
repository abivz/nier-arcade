using UnityEngine;

public class CachedMonoBehaviour : MonoBehaviour
{
    Transform _thisTransform;

    public Transform cachedTransform
    {
        get
        {
            if (_thisTransform == null)
                _thisTransform = GetComponent<Transform>();

            return _thisTransform;
        }
    }

    Rigidbody _thisRigidbody;

    public Rigidbody cachedRigidbody
    {
        get
        {
            if (_thisRigidbody == null)
                _thisRigidbody = GetComponent<Rigidbody>();

            return _thisRigidbody;
        }
    }

    Rigidbody2D _thisRigidbody2D;

    public Rigidbody2D cachedRigidbody2D
    {
        get
        {
            if (_thisRigidbody2D == null)
                _thisRigidbody2D = GetComponent<Rigidbody2D>();

            return _thisRigidbody2D;
        }
    }

    AudioSource _thisAudioSource;

    public AudioSource cachedAudioSource
    {
        get
        {
            if (_thisAudioSource == null)
                _thisAudioSource = GetComponent<AudioSource>();

            return _thisAudioSource;
        }
    }

    ParticleSystem _thisParticleSystem;

    public ParticleSystem cachedParticleSystem
    {
        get
        {
            if (_thisParticleSystem == null)
                _thisParticleSystem = GetComponent<ParticleSystem>();

            return _thisParticleSystem;
        }
    }
}