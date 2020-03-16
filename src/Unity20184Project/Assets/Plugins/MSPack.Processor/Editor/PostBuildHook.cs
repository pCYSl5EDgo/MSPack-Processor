// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MSPack.Processor.Core;
using MSPack.Processor.Core.Report;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace MSPack.Processor.Unity.Editor
{
    // ReSharper disable once UnusedMember.Global
    public class PostBuildHook : IPostBuildPlayerScriptDLLs
    {
        public static bool Enabled;
        private static readonly MethodInfo beginBuildStep;
        private static readonly MethodInfo endBuildStep;
        private readonly object[] msPackPostProcessor = { nameof(msPackPostProcessor) };
        private readonly object[] step = new object[1];

        static PostBuildHook()
        {
            beginBuildStep = typeof(BuildReport).GetMethod(nameof(beginBuildStep), BindingFlags.Instance | BindingFlags.NonPublic);
            endBuildStep = typeof(BuildReport).GetMethod(nameof(endBuildStep), BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public int callbackOrder { get; } = -1;

        public void OnPostBuildPlayerScriptDLLs(BuildReport report)
        {
            if(!Enabled)
            {
                return;
            }
            
            step[0] = beginBuildStep.Invoke(report, msPackPostProcessor);
            try
            {
                var relationshipGuids = AssetDatabase.FindAssets("t:" + nameof(RelationshipScriptableObject));
                if (relationshipGuids is null || relationshipGuids.Length == 0)
                {
                    ImplementDefault(report);
                }
                else
                {
                    var scriptableObjects = PrepareRelationShips(relationshipGuids);
                    Implement(report, scriptableObjects);
                }
            }
            finally
            {
                endBuildStep.Invoke(report, step);
            }
        }

        private static RelationshipScriptableObject[] PrepareRelationShips(string[] relationshipGuids)
        {
            var list = new List<RelationshipScriptableObject>(relationshipGuids.Length);
            foreach (var guid in relationshipGuids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var obj = AssetDatabase.LoadAssetAtPath<RelationshipScriptableObject>(assetPath);
                if (obj.IsEnabled)
                {
                    list.Add(obj);
                }
            }

            var scriptableObjects = list.ToArray();
            Array.Sort(scriptableObjects);
            return scriptableObjects;
        }

        private static readonly StringBuilder stringBuilder = new StringBuilder();

        private static string ToString(ref BuildFile file)
        {
            return stringBuilder.Clear()
#if UNITY_2019_3_OR_NEWER
                .Append("id : ").AppendLine(file.id.ToString())
#endif
                .Append("path : ").Append(file.path).Append("\nrole : ").Append(file.role).Append("\nsize : ").Append(file.size).ToString();
        }

        private static void ImplementDefault(BuildReport report)
        {
            var targetModule = default(string);
            var libraryList = new List<string>(128);
            var definitionList = new List<string>(64);
            var files = report.files;
            for (var index = 0; index < files.Length; index++)
            {
                ref var reportFile = ref files[index];
                var path = reportFile.path;
                switch (reportFile.role)
                {
                    case "ManagedLibrary":
                        if (path.EndsWith("Assembly-CSharp.dll"))
                        {
                            targetModule = path;
                        }
                        else
                        {
                            libraryList.Add(path);
                        }
                        break;
                    case "DependentManagedLibrary":
                    case "ManagedEngineAPI":
                        definitionList.Add(path);
                        break;
                    case "DebugInfo":
                        break;
                    default:
                        Debug.Log("DEFAULT\n" + ToString(ref reportFile));
                        break;
                }
            }

            if (targetModule is null)
            {
                return;
            }

            using (var generator = new CodeGenerator(Debug.Log, new NopReportHook()))
            {
                generator.Generate(targetModule, "", libraryList.ToArray(), definitionList.ToArray(), false, 0.75);
            }
        }

        private static void Implement(BuildReport report, RelationshipScriptableObject[] relationships)
        {
            var files = report.files;
            Action<string> logger = Debug.Log;
            IReportHook reportHook = new NopReportHook();

            foreach (var relationship in relationships)
            {
                ProcessEach(relationship, files, logger, reportHook);
            }
        }

        private static void ProcessEach(RelationshipScriptableObject relationship, BuildFile[] files, Action<string> logger, IReportHook reportHook)
        {
            var targetModule = FindTargetModule(relationship, files);
            var libraries = FindLibraries(relationship, files);
            var definitions = FindDefinitions(relationship, files);

            using (var generator = new CodeGenerator(logger, reportHook))
            {
                generator.Generate(targetModule, relationship.ResolverName, libraries, definitions, relationship.UseMapMode, relationship.HashtableLoadFactor);
            }
        }

        private static string[] FindDefinitions(RelationshipScriptableObject relationship, BuildFile[] files)
        {
            var definitions = new string[relationship.ReferenceDefinitionDllAbsoluteFilePaths.Length + relationship.ReferenceDefinitionNamesWithExtension.Length];
            relationship.ReferenceDefinitionDllAbsoluteFilePaths.CopyTo(definitions, 0);
            var enumerable = files.Select(x => x.path).Where(x => relationship.ReferenceDefinitionNamesWithExtension.Any(x.EndsWith));
            var index = relationship.ReferenceDefinitionDllAbsoluteFilePaths.Length;
            foreach (var buildFile in enumerable)
            {
                definitions[index++] = buildFile;
            }

            if (index != definitions.Length)
            {
                throw new FileNotFoundException(string.Join("\n", definitions) + "\n\nAbove were found.");
            }

            return definitions;
        }

        private static string[] FindLibraries(RelationshipScriptableObject relationship, BuildFile[] files)
        {
            var libraries = new string[relationship.ReferenceLibraryDllAbsoluteFilePaths.Length + relationship.ReferenceLibraryNamesWithExtension.Length];
            relationship.ReferenceLibraryDllAbsoluteFilePaths.CopyTo(libraries, 0);
            var enumerable = files.Select(x => x.path).Where(x => relationship.ReferenceLibraryNamesWithExtension.Any(x.EndsWith));
            var index = relationship.ReferenceLibraryDllAbsoluteFilePaths.Length;
            foreach (var buildFile in enumerable)
            {
                libraries[index++] = buildFile;
            }

            if (index != libraries.Length)
            {
                throw new FileNotFoundException(string.Join("\n", libraries) + "\n\nAbove were found.");
            }

            return libraries;
        }

        private static string FindTargetModule(RelationshipScriptableObject relationship, BuildFile[] files)
        {
            try
            {
                bool Predicate(BuildFile file) => file.path.EndsWith(relationship.InputNameWithExtension);

                return files.First(Predicate).path;
            }
            catch
            {
                throw new FileNotFoundException(relationship.InputNameWithExtension + " not found.");
            }
        }
    }
}
#endif
