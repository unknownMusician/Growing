using AreYouFruits.Nullability;

namespace Growing.Builder
{
    public sealed class PlacedBuildingInfoHolder
    {
        public Optional<BuildingInfo> CurrentBuildingInfo { get; set; }
    }
}