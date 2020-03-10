// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MSPack.Processor.Core;
using MSPack.Processor.Core.Report;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace MSPack.Processor.Unity.Editor
{
    // ReSharper disable once UnusedMember.Global
    public class PostBuildHook : IPostBuildPlayerScriptDLLs
    {
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
            step[0] = beginBuildStep.Invoke(report, msPackPostProcessor);
            try
            {
                Implement(report);
            }
            finally
            {
                endBuildStep.Invoke(report, step);
            }
        }

        private static readonly StringBuilder stringBuilder = new StringBuilder();

        private static string ToString(ref BuildFile file)
        {
            return stringBuilder.Clear().Append("id : ").Append(file.id).Append("\npath : ").Append(file.path).Append("\nrole : ").Append(file.role).Append("\nsize : ").Append(file.size).ToString();
        }

        private static void Implement(BuildReport report)
        {
            var targetModule = default(string);
            var libraryList = new List<string>(128);
            var definitionList = new List<string>(64);
            for (var index = 0; index < report.files.Length; index++)
            {
                ref var reportFile = ref report.files[index];
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
                Debug.Log("null!!!");
            }
            else
            {
                Debug.Log("pre process : " + targetModule);
                using (var generator = new CodeGenerator(Debug.Log, new NopReportHook()))
                {
                    Debug.Log("do?");
                    foreach (var path in libraryList)
                    {
                        Debug.Log("Library : " + libraryList);
                    }
                    generator.Generate(targetModule, "", libraryList.ToArray(), definitionList.ToArray(), false, 0.75);
                }
                Debug.Log("done!");
            }
        }
    }
}
#endif
