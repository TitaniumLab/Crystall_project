using UnityEngine;

namespace CrystalProject.EventBus.Signals
{
    public class CombineSignal
    {
        public readonly int CombinedUnitTier;
        public readonly Vector3 Position;

        public CombineSignal(int CombinedUnitTier, Vector3 pos)
        {
            this.CombinedUnitTier = CombinedUnitTier;
            Position = pos;
        }
    }
}