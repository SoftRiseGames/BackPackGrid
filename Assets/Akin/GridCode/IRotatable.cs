using System;

public interface IRotatable
{
    void RotateLeft(Action<int> callback);
    void RotateRight();
}
