using UnityEngine;

namespace Growing.Messages
{
    public struct BuildingPlacedEvent
    {
        public GameObject Building { get; set; }
        
        public BuildingPlacedEvent(GameObject building)
        {
            Building = building;
        }
    }
}