using UnityEngine;
public interface IProcessor 
{
    public bool IsLocked { get; set; }
    public bool IsEmpty { get; set; }
    void GetProduct(IProduct product);
    void ProcessStart();
    void ProcessUpdate();
    void ProcessEnd();
    void ProcessorUnlock();
    public Transform GetProductPlace();
}
