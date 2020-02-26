using System;
using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemCollectionsHashtableHelper
    {
        private readonly ModuleDefinition module;
        private readonly Func<IMetadataScope> hashtableScopeFunc;
        private TypeReference? hashtable;
        private MethodReference? _ctor;
        private MethodReference? _get_Item;
        private MethodReference? _Add;

        public SystemCollectionsHashtableHelper(ModuleDefinition module, Func<IMetadataScope> hashtableScopeFunc)
        {
            this.module = module;
            this.hashtableScopeFunc = hashtableScopeFunc;
        }

        public TypeReference Hashtable => hashtable ??= new TypeReference("System.Collections", "Hashtable", module, hashtableScopeFunc.Invoke(), false);

        public MethodReference Ctor => _ctor ??= new MethodReference(".ctor", module.TypeSystem.Void, Hashtable);

        public MethodReference get_Item => _get_Item ??= new MethodReference("get_Item", module.TypeSystem.Object, hashtable)
        {
            Parameters =
            {
                new ParameterDefinition("key", ParameterAttributes.None, module.TypeSystem.Object),
            },
        };

        public MethodReference Add => _Add ??= new MethodReference("Add", module.TypeSystem.Void, hashtable)
        {
            Parameters =
            {
                new ParameterDefinition("key", ParameterAttributes.None, module.TypeSystem.Object),
                new ParameterDefinition("value", ParameterAttributes.None, module.TypeSystem.Object),
            },
        };
    }
}
