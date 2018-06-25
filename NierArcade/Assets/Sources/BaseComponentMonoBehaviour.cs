using Entitas;
using UnityEngine;

public abstract class BaseComponentMonoBehaviour : MonoBehaviour
{
    public abstract int Index { get; }
    public abstract IComponent Component { get; }
}