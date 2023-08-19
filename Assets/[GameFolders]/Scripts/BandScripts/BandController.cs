using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandController : MonoBehaviour
{
    public List<Transform> HolderPoints;
    public List<GameObject> Holders;
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            var go = PoolingSystem.Instance.InstantiateAPS("Rope");
            Holders.Add(go);
            Holders[i].transform.position = HolderPoints[i].position;
        }

    }
    private void OnEnable()
    {
        EventManager.OnProductArriveNextSceneButton.AddListener(SortHolders);
    }
    private void OnDisable()
    {
        EventManager.OnProductArriveNextSceneButton.RemoveListener(SortHolders);
    }
    public void SortHolders()
    {

        for (int i = 0; i < Holders.Count; i++)
        {
            float distance = Vector3.Distance(Holders[i].transform.position, HolderPoints[i].position);
            if (distance > 0.1f)
                Holders[i].GetComponent<ProductHolder>().MovePoint(HolderPoints[i].position);
        }
    }
    public void AddHolder(GameObject newHolder)
    {
        if (Holders.Count == 3)
        {
            PoolingSystem.Instance.DestroyAPS(newHolder);
            return;
        }
        Holders.Add(newHolder);
        SortHolders();
    }
    public void RemoveHolder(GameObject removeHolder)
    {
        foreach (var item in Holders)
        {
            if (removeHolder == item)
            {
                Holders.Remove(removeHolder);
            }
        }
        SortHolders();
    }
}
