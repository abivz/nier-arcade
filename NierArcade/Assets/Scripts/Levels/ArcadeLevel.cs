using System;

using UnityEngine;

[Serializable]
public class ArcadeLevel : ScriptableObject
{
    //  Map
    public string Name;
    public string MapName;
    public AssetSource MapSource;
    public string MapBundleName;
    public int MapSizeX;
    public int MapSizeY;

    //  Objects
    public AssetSource DefaultSource;
    public string BundleName;
    public LevelObject[] Objects;
}
