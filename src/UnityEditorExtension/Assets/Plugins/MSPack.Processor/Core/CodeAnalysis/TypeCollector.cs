// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mono.Cecil;
using Mono.Collections.Generic;
using MSPack.Processor.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSPack.Processor.Core
{
    public class TypeCollector
    {
        private readonly ModuleDefinition module;
        private readonly bool useMapMode;
        private readonly Action<string> logger;
        private readonly List<TypeDefinition> unionInterfaceDefinitions = new List<TypeDefinition>();
        private readonly List<TypeDefinition> unionInterfaceGenericDefinitions = new List<TypeDefinition>();
        private readonly List<TypeDefinition> unionClassDefinitions = new List<TypeDefinition>();
        private readonly List<TypeDefinition> unionClassGenericDefinitions = new List<TypeDefinition>();
        private readonly List<TypeDefinition> classDefinitions = new List<TypeDefinition>();
        private readonly List<TypeDefinition> classGenericDefinitions = new List<TypeDefinition>();
        private readonly List<TypeDefinition> structDefinitions = new List<TypeDefinition>();
        private readonly List<TypeDefinition> structGenericDefinitions = new List<TypeDefinition>();

        public TypeCollector(ModuleDefinition module, bool useMapMode, Action<string> logger)
        {
            this.module = module;
            this.useMapMode = useMapMode;
            this.logger = logger;
            foreach (var typeDefinition in module.Types)
            {
                Visit(typeDefinition);
            }
            logger(module.Name + " : class = " + classDefinitions.Count + ", struct = " + structDefinitions.Count);
        }

        private void Visit(TypeDefinition type)
        {
            foreach (var typeNestedType in type.NestedTypes)
            {
                Visit(typeNestedType);
            }

            if (!type.HasCustomAttributes)
            {
                return;
            }
            logger("visit : " + type.FullName);
            var customAttributes = type.CustomAttributes;
            if (type.IsInterface)
            {
                VisitInterface(type, customAttributes);
            }
            else if (type.IsValueType)
            {
                VisitValueType(type, customAttributes);
            }
            else if (type.IsAbstract)
            {
                VisitAbstractClass(type, customAttributes);
            }
            else if (customAttributes.Any(CustomAttributeHelper.IsMessagePackObjectAttribute))
            {
                VisitMessagePackObjectAttributeClass(type);
            }
        }

        private void VisitMessagePackObjectAttributeClass(TypeDefinition type)
        {
            if (type.HasGenericParameters)
            {
                classGenericDefinitions.Add(type);
            }
            else
            {
                classDefinitions.Add(type);
            }
        }

        private void VisitAbstractClass(TypeDefinition type, Collection<CustomAttribute> customAttributes)
        {
            var isUnion = false;
            var isMessagePackObject = false;

            foreach (var attribute in customAttributes)
            {
                if (VisitCustomAttribute(type, attribute, ref isUnion, ref isMessagePackObject))
                {
                    return;
                }
            }
        }

        private bool VisitCustomAttribute(TypeDefinition typeDefinition, CustomAttribute attribute, ref bool isUnion, ref bool isMessagePackObject)
        {
            void AddUnion(TypeDefinition type)
            {
                if (type.HasGenericParameters)
                {
                    unionClassGenericDefinitions.Add(type);
                }
                else
                {
                    unionClassDefinitions.Add(type);
                }
            }

            if (CustomAttributeHelper.IsUnionAttribute(attribute))
            {
                isUnion = true;
                if (isMessagePackObject)
                {
                    AddUnion(typeDefinition);
                    return true;
                }
            }

            if (!CustomAttributeHelper.IsMessagePackObjectAttribute(attribute))
            {
                return false;
            }

            isMessagePackObject = true;
            if (!isUnion)
            {
                return false;
            }

            AddUnion(typeDefinition);
            return true;
        }

        private void VisitValueType(TypeDefinition type, Collection<CustomAttribute> customAttributes)
        {
            if (!customAttributes.Any(CustomAttributeHelper.IsMessagePackObjectAttribute))
            {
                logger("no messagepack obj : " + type.FullName);
                foreach (var attribute in type.CustomAttributes)
                {
                    logger(attribute.AttributeType.Name + " : " + attribute.AttributeType.Scope.Name);
                }
                return;
            }

            if (type.HasGenericParameters)
            {
                structGenericDefinitions.Add(type);
            }
            else
            {
                structDefinitions.Add(type);
            }
        }

        private void VisitInterface(TypeDefinition type, Collection<CustomAttribute> customAttributes)
        {
            if (!customAttributes.Any(CustomAttributeHelper.IsUnionAttribute))
            {
                return;
            }

            if (type.HasGenericParameters)
            {
                unionInterfaceGenericDefinitions.Add(type);
            }
            else
            {
                unionInterfaceDefinitions.Add(type);
            }
        }

        public CollectedInfo Collect()
        {
            var classInfos = CollectClassInfos();
            var structInfos = CollectStructInfos();
            var unionClassInfos = CollectUnionClassInfos();
            var interfaceInfos = CollectInterfaceInfos();

            return new CollectedInfo(
                module,
                classInfos,
                structInfos,
                unionClassInfos,
                interfaceInfos);
        }

        private UnionClassSerializationInfo[] CollectUnionClassInfos()
        {
            var infos = unionClassDefinitions.Count == 0 ? Array.Empty<UnionClassSerializationInfo>() : new UnionClassSerializationInfo[unionClassDefinitions.Count];

            for (var i = 0; i < unionClassDefinitions.Count; i++)
            {
                if (!UnionClassSerializationInfo.TryParse(unionClassDefinitions[i], out infos[i]))
                {
                    throw new InvalidOperationException(unionClassDefinitions[i].FullName);
                }
            }

            return infos;
        }

        private UnionInterfaceSerializationInfo[] CollectInterfaceInfos()
        {
            var infos = unionInterfaceDefinitions.Count == 0 ? Array.Empty<UnionInterfaceSerializationInfo>() : new UnionInterfaceSerializationInfo[unionInterfaceDefinitions.Count];

            for (var i = 0; i < unionInterfaceDefinitions.Count; i++)
            {
                if (!UnionInterfaceSerializationInfo.TryParse(unionInterfaceDefinitions[i], out infos[i]))
                {
                    throw new InvalidOperationException(unionInterfaceDefinitions[i].FullName);
                }
            }

            return infos;
        }

        private ClassSerializationInfo[] CollectClassInfos()
        {
            var infos = classDefinitions.Count == 0 ? Array.Empty<ClassSerializationInfo>() : new ClassSerializationInfo[classDefinitions.Count];

            for (var i = 0; i < classDefinitions.Count; i++)
            {
                if (!ClassSerializationInfo.TryParse(classDefinitions[i], useMapMode, out infos[i]))
                {
                    throw new InvalidOperationException(classDefinitions[i].FullName);
                }
            }

            return infos;
        }

        private StructSerializationInfo[] CollectStructInfos()
        {
            var infos = structDefinitions.Count == 0 ? Array.Empty<StructSerializationInfo>() : new StructSerializationInfo[structDefinitions.Count];

            for (var i = 0; i < structDefinitions.Count; i++)
            {
                if (!StructSerializationInfo.TryParse(structDefinitions[i], useMapMode, out infos[i]))
                {
                    throw new InvalidOperationException(structDefinitions[i].FullName);
                }
            }

            return infos;
        }
    }
}
