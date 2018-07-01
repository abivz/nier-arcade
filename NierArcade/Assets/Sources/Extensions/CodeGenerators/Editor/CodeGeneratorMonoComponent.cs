using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;
using DesperateDevs.Serialization;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins
{
    public class CodeGeneratorMonoComponent : AbstractGenerator
    {
        public override string name { get { return "ComponentMonoBehaviour"; } }

        const string COMPONENT_TEMPLATE =
            @"
using UnityEngine;
using Entitas;

public class ${Context}${ComponentType}MonoBehaviour : BaseComponentMonoBehaviour
{
                
    ${memberArgs}
    
    public override int Index
    {
        get
        {
            return ${ComponentIndex};
        }
    }

    public override IComponent Component
    {
        get 
        { 
            return new ${ComponentType}
            {
                ${memberAssignment}
            }; 
        }
    }
}
            ";
        
        const string MEMBER_ARGS_TEMPLATE =
            @"public ${MemberType} ${MemberName};";

        const string MEMBER_ASSIGNMENT_TEMPLATE =
            @"${memberName} = ${memberName},";

        public override CodeGenFile[] Generate(CodeGeneratorData[] data) {
            return generate(data
                .OfType<ComponentData>()
                .Where(d => d.ShouldGenerateIndex())
                .ToArray());
        }

        CodeGenFile[] generate(ComponentData[] data)
        {
            var contextNameToComponentData = data
                .Aggregate(new Dictionary<string, List<ComponentData>>(), (dict, d) => {
                    var contextNames = d.GetContextNames();
                    foreach (var contextName in contextNames) {
                        if (!dict.ContainsKey(contextName)) {
                            dict.Add(contextName, new List<ComponentData>());
                        }

                        dict[contextName].Add(d);
                    }

                    return dict;
                });

            foreach (var key in contextNameToComponentData.Keys.ToArray()) {
                contextNameToComponentData[key] = contextNameToComponentData[key]
                    .OrderBy(d => d.GetTypeName())
                    .ToList();
            }

            return contextNameToComponentData
                    .SelectMany(d => generateComponentMonoBehaviours(d.Key, d.Value.ToArray()))
                    .ToArray();
        }

        CodeGenFile[] generateComponentMonoBehaviours(string contextName, ComponentData[] data) {
            return data
                    .Select((d, index) => generateComponentMonoBehaviousClass(contextName, d, index))
                    .ToArray();
        }

        CodeGenFile generateComponentMonoBehaviousClass(string contextName, ComponentData data, int index) {
            var memberData = data.GetMemberData();
            var componentType = data.GetTypeName();

            return new CodeGenFile(
                contextName + Path.DirectorySeparatorChar + "ComponentsMonoBehaviours" + Path.DirectorySeparatorChar + contextName + componentType + "MonoBehaviour.cs",
                    COMPONENT_TEMPLATE
                    .Replace("${Context}", contextName)
                    .Replace("${ComponentType}", componentType)
                    .Replace("${ComponentIndex}", index.ToString())
                    .Replace("${memberArgs}", getMemberArgs(memberData))
                    .Replace("${memberAssignment}", getMemberAssignment(memberData)),
                GetType().FullName
            );
        }
        
        string getMemberArgs(MemberData[] memberData) {
            var args = memberData
                .Select(info => MEMBER_ARGS_TEMPLATE
                    .Replace("${MemberType}", info.type)
                    .Replace("${MemberName}", info.name)
                )
                .ToArray();

            return string.Join("\n\t", args);
        }

        string getMemberAssignment(MemberData[] memberData) {
            var assignments = memberData
                .Select(info => MEMBER_ASSIGNMENT_TEMPLATE
                    .Replace("${MemberType}", info.type)
                    .Replace("${memberName}", info.name)
                    .Replace("${MemberName}", info.name)
                )
                .ToArray();

            return string.Join("\n\t\t\t\t", assignments).TrimEnd(',');
        }
    }
}
