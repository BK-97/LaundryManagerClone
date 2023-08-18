using UnityEngine;
public interface IProduct
{
    void Sell();
    void MoveProcess(IProcessor processor);
    void MoveProcessEnd(IProcessor processor);
    void MoveNextProcessPlatform();
}
