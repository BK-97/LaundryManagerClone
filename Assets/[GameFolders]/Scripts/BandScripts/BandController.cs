using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandController : MonoBehaviour
{
    public List<Transform> HolderPoints;
    public List<GameObject> Holders;
    public bool colorBoil;
    private void OnEnable()
    {
        EventManager.OnProductArriveNextSceneButton.AddListener(RemoveHolder);
    }
    private void OnDisable()
    {
        EventManager.OnProductArriveNextSceneButton.RemoveListener(RemoveHolder);
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
        Holders.Add(newHolder);
        newHolder.GetComponent<ProductHolder>().ReadyOnBand();
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
        if (Holders.Count == 0)
            EventManager.OnRawMatEnd.Invoke();
        SortHolders();
    }
}
