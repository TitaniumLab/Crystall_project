using System;

public interface ILossSender
{
    public static event Action<float> LossCountChanged;
    public static void Invoke(float ) =>
        LossCountChanged(isCount);
}
