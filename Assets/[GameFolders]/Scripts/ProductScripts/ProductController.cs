using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ProductController : MonoBehaviour,IProduct
{
    #region Params
    public ProductHolder holder;
    #endregion
    #region IProductMethods
    public void MoveProcess(IProcessor processor)
    {
        Debug.Log("MoveProcessStart");

        Transform targetTransform = processor.GetProductPlace();
        transform.DOMove(targetTransform.position, 1).SetEase(Ease.Linear).OnComplete(() => MoveProcessEnd(processor));
    }
    public void MoveProcessEnd(IProcessor processor)
    {
        holder.Demolish();
        processor.GetProduct();
        Destroy(gameObject);
    }
    public void MoveNextProcessPlatform()
    {
        Vector3 targetPos = GameManager.Instance.NextSceneUIPos;
        transform.DORotate(Vector3.zero,0.5f);
        transform.DOMove(targetPos, 1).SetEase(Ease.Linear).OnComplete(ProductNextScene);
    }
    public void Sell()
    {
        Destroy(gameObject);
    }
    #endregion
    #region MyMethods
    private void ProductNextScene()
    {
        PoolingSystem.Instance.DestroyAPS(gameObject);
        EventManager.OnProductArriveNextSceneButton.Invoke();
    }

    #endregion
}
