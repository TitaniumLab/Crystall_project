namespace CrystalProject.EventBus.Signals
{
    public class ShakeSignal
    {
        public float ShakeStrength;
        public ShakeSignal(float shakeStrength)
        {
            ShakeStrength = shakeStrength;
        }
    }
}