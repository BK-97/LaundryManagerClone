using UnityEngine;
public interface IProduct
{
    int ProductWorth { get; set; }
    void SetInfo(EnumTypes.ProductTypes proType,EnumTypes.ColorTypes colorType,int addWorth);
    void MoveProcess(IProcessor processor);
    void MoveNextProcess();
    void Sell();

}
