// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using UnityEngine;

namespace MSPack.Processor.Unity.Editor
{
    [CreateAssetMenu(menuName = "MSPack/Relationship")]
    public class RelationshipScriptableObject : ScriptableObject, IComparable<RelationshipScriptableObject>
    {
        public bool IsEnabled;

        public int Order;

        public string InputNameWithExtension;

        public string ResolverName;

        public string[] ReferenceLibraryNamesWithExtension;

        public string[] ReferenceLibraryDllAbsoluteFilePaths;

        public string[] ReferenceDefinitionNamesWithExtension;

        public string[] ReferenceDefinitionDllAbsoluteFilePaths;

        public bool UseMapMode;

        public double HashtableLoadFactor;

        public RelationshipScriptableObject()
        {
            HashtableLoadFactor = 1f;
            UseMapMode = false;
            ReferenceDefinitionDllAbsoluteFilePaths = Array.Empty<string>();
            ReferenceDefinitionNamesWithExtension = Array.Empty<string>();
            ReferenceLibraryDllAbsoluteFilePaths = Array.Empty<string>();
            ReferenceLibraryNamesWithExtension = Array.Empty<string>();
            ResolverName = string.Empty;
            InputNameWithExtension = string.Empty;
            Order = 0;
            IsEnabled = true;
        }

        public int CompareTo(RelationshipScriptableObject other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return Order.CompareTo(other.Order);
        }
    }
}