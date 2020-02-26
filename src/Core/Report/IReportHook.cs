namespace MSPack.Processor.Core.Report
{
    public interface IReportHook
    {
        void ImportNotPreviouslyReferencedAssemblyNameReference(string name, int majorVersion, int minorVersion, int buildVersion, int revisionVersion, string culture, byte[] publicKeyToken);
    }
}
