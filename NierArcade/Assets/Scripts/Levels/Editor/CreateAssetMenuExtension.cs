using UnityEditor;

public class CreateAssetMenuExtension : Editor
{
    [MenuItem("Assets/Create/Arcade/Level")]
    static void CreateLevelAsset()
    {
        ScriptableObjectUtility.CreateAsset<ArcadeLevel>();
    }
}
