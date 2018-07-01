using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins {

    public class CodeGeneratorGameObjectTag : ICodeGenerator {

        public string name { get { return "GameObjectTag"; } }
        public int priority { get { return 0; } }
        public bool runInDryMode { get { return true; } }

        const string TEMPLATE =
@"[System.Serializable]
public struct GameObjectTag
{
    public GameObjectTagEnum Value;

    public override string ToString()
    {
${TagConditions}
${ReturnDefaultTag}
    }

${TagStatic}

    public static bool operator ==(string str, GameObjectTag tag) 
    {
        return str == tag.ToString();
    }

    public static bool operator !=(string str, GameObjectTag tag) 
    {
        return str != tag.ToString();
    }

    public static bool operator ==(GameObjectTag tag, string str) 
    {
        return tag.ToString() == str;
    }

    public static bool operator !=(GameObjectTag tag, string str) 
    {
        return tag.ToString() != str;
    }

    public static bool operator ==(GameObjectTagEnum tagEnum, GameObjectTag tag) 
    {
        return tagEnum == tag.Value;
    }

    public static bool operator !=(GameObjectTagEnum tagEnum, GameObjectTag tag) 
    {
        return tagEnum != tag.Value;
    }

    public static bool operator ==(GameObjectTag tag, GameObjectTagEnum tagEnum) 
    {
        return tag.Value == tagEnum;
    }

    public static bool operator !=(GameObjectTag tag, GameObjectTagEnum tagEnum) 
    {
        return tag.Value != tagEnum;
    }

    public static bool operator ==(GameObjectTag tag1, GameObjectTag tag2) 
    {
        return tag1.Value == tag2.Value;
    }

    public static bool operator !=(GameObjectTag tag1, GameObjectTag tag2) 
    {
        return tag1.Value != tag2.Value;
    }
}

public enum GameObjectTagEnum
{
${Tags}
}
";

        const string TAG_CONDITION_TEMPLATE =
@"        if (Value == GameObjectTagEnum.${tagEnum})
            return ""${tagString}"";
";

        const string RETURN_DEFAULT_TEMPLATE = @"        return ""${default}"";";

        const string TAG_STATIC_TEMPLATE =
@"    public static GameObjectTag ${tagEnum}
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.${tagEnum}
            };
        }
    }";

        const string TAG_TEMPLATE = @"    ${tagEnum}";

        public CodeGenFile[] Generate(CodeGeneratorData[] data) {
            return new[] {
                new CodeGenFile(
                    "GameObjectTag.cs",
                    generate(UnityEditorInternal.InternalEditorUtility.tags),
                    GetType().FullName)
            };
        }

        string generate(string[] tags) {
            var tagConditionList = string.Join("\n", tags
                .Select(tag => TAG_CONDITION_TEMPLATE
                    .Replace("${tagEnum}", TagStringToTagEnum(tag))
                    .Replace("${tagString}", tag))
                .ToArray());

            var returnDefaultTag = RETURN_DEFAULT_TEMPLATE.Replace("${default}", "Untagged");

            var tagStaticList = string.Join("\n", tags
                .Select(tag => TAG_STATIC_TEMPLATE.Replace("${tagEnum}", TagStringToTagEnum(tag)))
                .ToArray());

            var tagEnumList = string.Join(",\n", tags
                .Select(tag => TAG_TEMPLATE.Replace("${tagEnum}", TagStringToTagEnum(tag)))
                .ToArray());

            return TEMPLATE
                .Replace("${TagConditions}", tagConditionList)
                .Replace("${ReturnDefaultTag}", returnDefaultTag)
                .Replace("${TagStatic}", tagStaticList)
                .Replace("${Tags}", tagEnumList);
        }

        string TagStringToTagEnum(string tag)
        {
            return "Tag_" + tag.Replace(" ", "_");
        }
    }
}