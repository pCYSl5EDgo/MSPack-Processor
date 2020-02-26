// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemCollectionsGenericIEqualityComparerHelper
    {
        private readonly ModuleDefinition module;
        private readonly ModuleImporter importer;
        private readonly IMetadataScope coreLibraryScope;
        private TypeReference? iEqualityComparerBase;

        public SystemCollectionsGenericIEqualityComparerHelper(ModuleDefinition module, ModuleImporter importer)
        {
            this.module = module;
            this.importer = importer;
            coreLibraryScope = module.TypeSystem.CoreLibrary;
        }

        public TypeReference IEqualityComparerBase
        {
            get
            {
                if (iEqualityComparerBase == null)
                {
                    iEqualityComparerBase = new TypeReference("System.Collections.Generic", "IEqualityComparer`1", module, coreLibraryScope, false);
                    iEqualityComparerBase.GenericParameters.Add(new GenericParameter("T", iEqualityComparerBase));
                }

                return iEqualityComparerBase;
            }
        }

        private readonly List<(TypeReference tType, GenericInstanceType answerType)> memoIEqualityComparer = new List<(TypeReference tType, GenericInstanceType answerType)>();

        public GenericInstanceType IEqualityComparerGeneric(TypeReference typeReference)
        {
            foreach (var (tType, answerType) in memoIEqualityComparer)
            {
                if (ReferenceEquals(tType, typeReference))
                {
                    return answerType;
                }
            }

            var importedT = importer.Import(typeReference);
            var answer = new GenericInstanceType(IEqualityComparerBase)
            {
                GenericArguments = { importedT, },
            };
            memoIEqualityComparer.Add((typeReference, answer));
            return answer;
        }
    }
}
