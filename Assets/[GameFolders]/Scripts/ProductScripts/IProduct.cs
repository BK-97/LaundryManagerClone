public interface IProduct
{
    public ProductData GetProductData();
    void Sell();
    void MoveProcess(IProcessor processor);
    void ProcessEnd();
}
