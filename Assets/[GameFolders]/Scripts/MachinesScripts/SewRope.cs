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
        StartCoroutine(WorkCoroutine(workTime));
        currentIndex = 0;
        for (int i = 0; i < ropes.Count; i++)
        {
            ropes[i].transform.localScale = Vector3.one;
        }
    }

    private IEnumerator WorkCoroutine(float workTime)
    {
        float interval = workTime/ropes.Count ;

        while (ropes.Count > 0)
        {
            yield return new WaitForSeconds(interval);
            RopeDestroy(interval-0.1f);
        }
    }
    private void RopeDestroy(float destroyTime)
    {
        ropes[currentIndex].transform.DOScale(Vector3.one*0.5f, destroyTime).SetEase(Ease.InCirc);
        currentIndex++;
    }
}
