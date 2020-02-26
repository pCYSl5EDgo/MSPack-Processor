// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemCollectionsGenericDictionaryHelper
    {
        private readonly ModuleDefinition module;
        private readonly Func<IMetadataScope> dictionaryScopeFunc;
        private readonly SystemCollectionsGenericIEqualityComparerHelper iEqualityComparerHelper;
        private readonly ModuleImporter importer;
        private TypeReference? dictionaryBase;

        public SystemCollectionsGenericDictionaryHelper(ModuleDefinition module, Func<IMetadataScope> dictionaryScopeFunc, SystemCollectionsGenericIEqualityComparerHelper iEqualityComparerHelper, ModuleImporter importer)
        {
            this.module = module;
            this.dictionaryScopeFunc = dictionaryScopeFunc;
            this.iEqualityComparerHelper = iEqualityComparerHelper;
            this.importer = importer;
        }

        public TypeReference DictionaryBase
        {
            get
            {
                if (dictionaryBase is null)
                {
                    dictionaryBase = new TypeReference("System.Collections.Generic", "Dictionary`2", module, dictionaryScopeFunc.Invoke(), false);
                    dictionaryBase.GenericParameters.Add(new GenericParameter("TKey", dictionaryBase));
                    dictionaryBase.GenericParameters.Add(new GenericParameter("TValue", dictionaryBase));
                }

                return dictionaryBase;
            }
        }

        private readonly List<(TypeReference key, TypeReference value, GenericInstanceType answer)> memoDictionary = new List<(TypeReference key, TypeReference value, GenericInstanceType answer)>();

        public GenericInstanceType DictionaryGeneric(TypeReference key, TypeReference value)
        {
            foreach (var (keyType, valueType, answerType) in memoDictionary)
            {
                if (ReferenceEquals(key, keyType) && ReferenceEquals(value, valueType))
                {
                    return answerType;
                }
            }

            var importedKey = importer.Import(key);
            var importedValue = importer.Import(value);
            var answer = new GenericInstanceType(DictionaryBase)
            {
                GenericArguments =
                {
                    importedKey,
                    importedValue,
                },
            };

            memoDictionary.Add((key, value, answer));

            return answer;
        }

        private readonly List<(GenericInstanceType dictionaryType, MethodReference answerMethod)> memoTryGetValue = new List<(GenericInstanceType dictionaryType, MethodReference answerMethod)>();

        public MethodReference TryGetValue(GenericInstanceType dictionary)
        {
            foreach (var (dictionaryType, answerMethod) in memoTryGetValue)
            {
                if (ReferenceEquals(dictionaryType, dictionary))
                {
                    return answerMethod;
                }
            }

            var answer = new MethodReference("TryGetValue", module.TypeSystem.Boolean, dictionary)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("key", ParameterAttributes.None, DictionaryBase.GenericParameters[0]),
                    new ParameterDefinition("value", ParameterAttributes.Out, new ByReferenceType(DictionaryBase.GenericParameters[1])),
                },
            };
            memoTryGetValue.Add((dictionary, answer));
            return answer;
        }

        private readonly List<(GenericInstanceType dictionaryType, MethodReference answerMethod)> memoAdd = new List<(GenericInstanceType dictionaryType, MethodReference answerMethod)>();

        public MethodReference Add(GenericInstanceType dictionary)
        {
            foreach (var (dictionaryType, answerMethod) in memoAdd)
            {
                if (ReferenceEquals(dictionaryType, dictionary))
                {
                    return answerMethod;
                }
            }

            var answer = new MethodReference("Add", module.TypeSystem.Void, dictionary)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("key", ParameterAttributes.None, DictionaryBase.GenericParameters[0]),
                    new ParameterDefinition("value", ParameterAttributes.None, DictionaryBase.GenericParameters[1]),
                },
            };
            memoAdd.Add((dictionary, answer));
            return answer;
        }

        private readonly List<(TypeReference dictionaryType, MethodReference answerMethod)> memoCtorCapacity = new List<(TypeReference dictionary, MethodReference answer)>();

        public MethodReference Ctor_CapacityGeneric(GenericInstanceType dictionary)
        {
            foreach (var (dictionaryType, answerMethod) in memoCtorCapacity)
            {
                if (ReferenceEquals(dictionaryType, dictionary))
                {
                    return answerMethod;
                }
            }

            var answer = new MethodReference(".ctor", module.TypeSystem.Void, dictionary)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("capacity", ParameterAttributes.None, module.TypeSystem.Int32),
                },
            };
            memoCtorCapacity.Add((dictionary, answer));

            return answer;
        }

        private readonly List<(TypeReference dictionaryType, MethodReference answerMethod)> memoCtorCapacityEqualityComparer = new List<(TypeReference dictionary, MethodReference answer)>();

        public MethodReference Ctor_Capacity_EqualityComparerGeneric(GenericInstanceType dictionary)
        {
            foreach (var (dictionaryType, answerMethod) in memoCtorCapacityEqualityComparer)
            {
                if (ReferenceEquals(dictionaryType, dictionary))
                {
                    return answerMethod;
                }
            }

            var answer = new MethodReference(".ctor", module.TypeSystem.Void, dictionary)
            {
                HasThis = true,
                Parameters =
                {
                    new ParameterDefinition("capacity", ParameterAttributes.None, module.TypeSystem.Int32),
                    new ParameterDefinition("comparer", ParameterAttributes.None, iEqualityComparerHelper.IEqualityComparerGeneric(DictionaryBase.GenericParameters[0])),
                },
            };
            memoCtorCapacityEqualityComparer.Add((dictionary, answer));

            return answer;
        }
    }
}
