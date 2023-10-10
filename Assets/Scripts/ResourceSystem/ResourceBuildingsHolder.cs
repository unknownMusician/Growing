using System.Collections.Generic;

namespace Growing.ResourceSystem
{
    public sealed class ResourceBuildingsHolder
    {
        public ICollection<ResourceCreator> ResourceCreators { get; } = new HashSet<ResourceCreator>();
    }
}