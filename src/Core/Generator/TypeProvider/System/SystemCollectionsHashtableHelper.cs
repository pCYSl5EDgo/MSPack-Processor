using System;
using Mono.Cecil;

namespace MSPack.Processor.Core.Provider
{
    public sealed class SystemCollectionsHashtableHelper
    {
        private readonly ModuleDefinition module;
        private readonly Func<IMetadataScope> hashtableScopeFunc;

#if CSHARP_8_0_OR_NEWER
        private TypeReference? hashtable;
        private MethodReference? _ctor;
        private MethodReference? _get_Item;
        private MethodReference? _Add;
#else
        private TypeReference hashtable;
        private MethodReference _ctor;
        private MethodReference _get_Item;
        private MethodReference _Add;
#endif

        public SystemCollectionsHashtableHelper(ModuleDefinition module, Func<IMetadataScope> hashtableScopeFunc)
        {
            this.module = module;
            this.hashtableScopeFunc = hashtableScopeFunc;
        }

        public TypeReference Hashtable
        {
            get
            {
                if (hashtable == null)
                {
                    hashtable = new TypeReference("System.Collections", "Hashtable", module, hashtableScopeFunc.Invoke(), false);
                }

                return hashtable;
            }
        }

        public MethodReference Ctor
        {
            get
            {
                if (_ctor == null)
                {
                    _ctor = new MethodReference(".ctor", module.TypeSystem.Void, Hashtable);
                }

                return _ctor;
            }
        }

        public MethodReference get_Item
        {
            get
            {
                if (_get_Item == null)
                {
                    _get_Item = new MethodReference("get_Item", module.TypeSystem.Object, hashtable)
                    {
                        Parameters =
                        {
                            new ParameterDefinition("key", ParameterAttributes.None, module.TypeSystem.Object),
                        },
                    };
                }

                return _get_Item;
            }
        }

        public MethodReference Add
        {
            get
            {
                if (_Add == null)
                {
                    _Add = new MethodReference("Add", module.TypeSystem.Void, hashtable)
                    {
                        Parameters =
                        {
                            new ParameterDefinition("key", ParameterAttributes.None, module.TypeSystem.Object),
                            new ParameterDefinition("value", ParameterAttributes.None, module.TypeSystem.Object),
                        },
                    };
                }

                return _Add;
            }
        }
    }
}
