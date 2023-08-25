using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SewRope : MonoBehaviour
{
    public List<GameObject> ropes;
    private int currentIndex;
    public void StartWorking(float workTime)
    {
        currentIndex = 0;
        for (int i = 0; i < ropes.Count; i++)
        {
            ropes[i].transform.localScale = Vector3.one;
        }
        StartCoroutine(WorkCoroutine(workTime));

    }

    private IEnumerator WorkCoroutine(float workTime)
    {
        float interval = workTime/ropes.Count;
        while (ropes.Count > 0)
        {
            RopeDestroy(interval - 0.01f);
            yield return new WaitForSeconds(interval);
        }
    }
    private void RopeDestroy(float destroyTime)
    {
        ropes[currentIndex].transform.DOScale(Vector3.zero, destroyTime).SetEase(Ease.InBack);
        currentIndex++;
    }
}
