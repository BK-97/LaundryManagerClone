using UnityEngine;
public interface IProcessor 
{
    public bool IsLocked { get; set; }
    public bool OnProcess { get; set; }
    void GetProduct(EnumTypes.ProductTypes proType,EnumTypes.ColorTypes colorType);
    void ProcessStart();
    void ProcessUpdate();
    void ProcessEnd();
    void ProcessorUnlock();
    public Transform GetProductPlace();

}
