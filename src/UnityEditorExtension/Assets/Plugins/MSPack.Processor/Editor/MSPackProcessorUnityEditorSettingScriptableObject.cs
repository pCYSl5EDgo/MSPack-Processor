// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if UNITY_EDITOR
using UnityEngine;

namespace MSPack.Processor.Unity.Editor
{
    [CreateAssetMenu(menuName = "MSPack/Setting")]
    public class MSPackProcessorUnityEditorSettingScriptableObject : ScriptableObject
    {
        public bool Enabled { get; set; }

#if CSHARP_8_0_OR_NEWER
        public string? InputAssembly { get; set; }

        public string[]? LibraryAssemblyNames { get; set; }

        public string[]? DefinitionAssemblyNames { get; set; }
#else
        public string InputAssembly { get; set; }
     
        public string[] LibraryAssemblyNames { get; set; }

        public string[] DefinitionAssemblyNames { get; set; }
#endif
    }
}
#endif
