using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ProductController : MonoBehaviour,IProduct
{
    #region Params
    #endregion
    #region IProductMethods

    public void MoveProcess(IProcessor processor)
    {
        Transform targetTransform = processor.GetProductPlace();
        transform.SetParent(null);
        transform.DOMove(targetTransform.position, 1).SetEase(Ease.Linear).OnComplete(() => MoveProcessEnd(processor));
    }
    public void MoveProcessEnd(IProcessor processor)
    {
        processor.GetProduct();
        Destroy(gameObject);
    }
    
    public void MoveNextProcessPlatform()
    {
        Vector3 targetPos = GameManager.Instance.NextSceneUIPos;
        transform.DOMove(targetPos, 1).SetEase(Ease.Linear).OnComplete(() => EventManager.OnProductArriveNextSceneButton.Invoke());
    }
    public void Sell()
    {
        Destroy(gameObject);
    }
    #endregion
    #region MyMethods
    private void OnEnable()
    {
        EventManager.OnProductArriveNextSceneButton.AddListener(()=>Destroy(gameObject));
    }
    private void OnDisable()
    {
        EventManager.OnProductArriveNextSceneButton.RemoveListener(() => Destroy(gameObject));

    }
    #endregion
}
