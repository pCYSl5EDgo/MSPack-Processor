// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace MSPack.Processor.Core.Report
{
    public sealed class NopReportHook : IReportHook
    {
        public void ImportNotPreviouslyReferencedAssemblyNameReference(string name, int majorVersion, int minorVersion, int buildVersion, int revisionVersion, string culture, byte[] publicKeyToken)
        {
        }
    }
}
