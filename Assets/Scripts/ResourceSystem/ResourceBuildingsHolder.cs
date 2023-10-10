using System.Collections.Generic;

namespace Growing.ResourceSystem
{
    public sealed class ResourceBuildingsHolder
    {
        public ICollection<ResourceTransferer> ResourceTransferers { get; } = new HashSet<ResourceTransferer>();
    }
}