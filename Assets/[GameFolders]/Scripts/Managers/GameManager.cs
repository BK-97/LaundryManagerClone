using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameConfig
{
    public bool IsLooping
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefKeys.IsLooping, 0) != 0;
        }

        set
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.IsLooping, 1);
        }
    }
}
public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public UnityEvent OnGameStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnGameEnd = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStageSuccess = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStageFail = new UnityEvent();

    private bool isGameStarted;
    public bool IsGameStarted { get { return isGameStarted; } set { isGameStarted = value; } }

    private bool isStageCompleted;
    public bool IsStageCompleted { get { return isStageCompleted; } set { isStageCompleted = value; } }

    public GameConfig GameConfig;
    [HideInInspector]
    public Vector3 NextSceneUIPos;
    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(() => IsStageCompleted = false);
    }

    private void OnDisable()
    {
        SceneController.Instance.OnSceneLoaded.RemoveListener(() => IsStageCompleted = false);
    }
    public void StartGame()
    {
        if (isGameStarted)
            return;

        isGameStarted = true;
        OnGameStart.Invoke();
    }
    public void EndGame()
    {
        if (!isGameStarted)
            return;
        isGameStarted = false;
        OnGameEnd.Invoke();
    }
    public void CompeleteStage(bool value)
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;

        if (IsStageCompleted == true)
            return;

        if (value)
        {
            StartCoroutine(WaitLevelChange(value));
            OnStageSuccess.Invoke();
        }
        else
        {
            StartCoroutine(WaitLevelChange(value));
            OnStageFail.Invoke();
        }
        IsStageCompleted = true;
    }

    IEnumerator WaitLevelChange(bool status)
    {
        yield return new WaitForSeconds(2);
        if (status)
            LevelManager.Instance.LoadNextLevel();
        else
            LevelManager.Instance.ReloadLevel();


    }
}