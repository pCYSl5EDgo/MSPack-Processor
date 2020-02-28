// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Cysharp.Text;
using MSPack.Processor.Core.Report;
using System.Globalization;

namespace MSPack.Processor.CLI
{
    public class ReportHook : IReportHook
    {
        private readonly DepsJson json;

        public ReportHook(DepsJson json)
        {
            this.json = json;
        }

        private static string CalcVersion(int major, int minor, int build)
        {
            var builder = ZString.CreateStringBuilder();
            builder.Append(major);
            builder.Append('.');
            builder.Append(minor);
            builder.Append('.');
            builder.Append(build);
            var str = builder.ToString();
            builder.Dispose();
            return str;
        }

        public void ImportNotPreviouslyReferencedAssemblyNameReference(string name, int majorVersion, int minorVersion, int buildVersion, int revisionVersion, string culture, byte[] publicKeyToken)
        {

        }
    }
}
