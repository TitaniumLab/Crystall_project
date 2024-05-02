using System;

namespace CrystalProject.Loss
{
    public interface ILossSender
    {
        public static event Action<float> LossCountChanged;
        public static void Invoke(float value) =>
            LossCountChanged(value);
    }
}
