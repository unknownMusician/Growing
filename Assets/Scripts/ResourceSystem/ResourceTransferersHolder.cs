using System.Collections.Generic;

namespace Growing.ResourceSystem
{
    public sealed class ResourceTransferersHolder
    {
        public ICollection<ResourceTransferer> Values { get; } = new HashSet<ResourceTransferer>();
    }
}