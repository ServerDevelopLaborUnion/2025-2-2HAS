using System;

namespace MehodArchive
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Delegate)]
    public class ArchiveMethodAttribute : Attribute
    {
        public string methodName;

        public ArchiveMethodAttribute(string methodName)
        {
            this.methodName = methodName;
        }
    }
}
