#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace MSPack.Processor.Unity.Editor
{
    public class PostBuildHook : IPostBuildPlayerScriptDLLs
    {
        private static readonly MethodInfo BeginBuildStep;
        private static readonly MethodInfo EndBuildStep;
        private readonly object[] uniEnumExtension = { nameof(uniEnumExtension) };
        private readonly object[] step = new object[1];

        static PostBuildHook()
        {
            BeginBuildStep = typeof(BuildReport).GetMethod(nameof(BeginBuildStep), BindingFlags.Instance | BindingFlags.NonPublic);
            EndBuildStep = typeof(BuildReport).GetMethod(nameof(EndBuildStep), BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public int callbackOrder { get; } = -1;

        public void OnPostBuildPlayerScriptDLLs(BuildReport report)
        {
            step[0] = BeginBuildStep.Invoke(report, uniEnumExtension);
            try
            {
                Implement(report);
            }
            finally
            {
                EndBuildStep.Invoke(report, step);
            }
        }

        private static StringBuilder builder = new StringBuilder();

        private static string ToString(ref BuildFile file)
        {
            return builder.Clear().Append("id : ").Append(file.id).Append("\npath : ").Append(file.path).Append("\nrole : ").Append(file.role).Append("\nsize : ").Append(file.size).ToString();
        }

        private void Implement(BuildReport report)
        {
            var definitionList = new List<string>(64);
            var libraryList = new List<string>(128);
            for (var index = 0; index < report.files.Length; index++)
            {
                ref var reportFile = ref report.files[index];
                var path = reportFile.path;
                Debug.Log(ToString(ref reportFile));
                switch (reportFile.role)
                {
                    case "ManagedLibrary":
                        libraryList.Add(path);
                        break;
                    case "DependentManagedLibrary":
                    case "ManagedEngineAPI":
                        definitionList.Add(path);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
#endif
