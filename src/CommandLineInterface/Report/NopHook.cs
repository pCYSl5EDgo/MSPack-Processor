using MSPack.Processor.Core.Report;

namespace MSPack.Processor.CLI
{
    public class NopHook : IReportHook
    {
        public void ImportNotPreviouslyReferencedAssemblyNameReference(string name, int majorVersion, int minorVersion, int buildVersion, int revisionVersion, string culture, byte[] publicKeyToken)
        {
        }
    }
}
