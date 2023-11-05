using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BandController : MonoBehaviour
{
    public BandController nextBand;
    public List<Transform> HolderPoints;
    public List<GameObject> Holders;
    private bool isBandFull;
    public bool isFirstBand;
    public GameObject HolderPrefab;
    private void Start()
    {
        if (isFirstBand)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnRawResources();
            }
        }
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
        newHolder.transform.position = HolderPoints[Holders.Count].position;
        newHolder.transform.rotation = HolderPoints[Holders.Count].rotation;
        Holders.Add(newHolder);
        newHolder.GetComponent<ProductHolder>().bandController=this;
        newHolder.GetComponent<ProductHolder>().OnBand();
        if (Holders.Count == 3)
            isBandFull = true;

    }
    public void RemoveHolder(GameObject removeHolder)
    {
        for (int i = Holders.Count - 1; i >= 0; i--)
        {
            if (removeHolder == Holders[i])
            {
                Holders.RemoveAt(i);
            }
        }

        if (Holders.Count < 3)
            isBandFull = false;
        if (isFirstBand && Holders.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnRawResources();
            }
        }
            
        SortHolders();
    }
    private void SpawnRawResources()
    {
        if (isBandFull)
            return;
        if (!isFirstBand)
            return;

        //dotween ile saðdan smooth þekilde yerine yerleþtir
        var go = Instantiate(HolderPrefab);
        go.GetComponent<ProductHolder>().SetInfo(EnumTypes.ProductTypes.Rope, EnumTypes.ColorTypes.None, 0);
        AddHolder(go);
        go.GetComponent<ProductHolder>().OnBand();
    }
}
