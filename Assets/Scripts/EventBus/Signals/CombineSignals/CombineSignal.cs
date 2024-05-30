using UnityEngine;

namespace CrystalProject.EventBus.Signals
{
    public class CombineSignal
    {
        public readonly int CombinedUnitTier;
        public readonly Vector3 FirstPos;
        public readonly Vector3 SecondPos;

        public CombineSignal(int CombinedUnitTier, Vector3 pos1, Vector3 pos2 )
        {
            this.CombinedUnitTier = CombinedUnitTier;
            FirstPos = pos1;
            SecondPos = pos2;
        }
    }
}