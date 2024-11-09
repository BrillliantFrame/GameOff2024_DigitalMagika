using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppCore : Singleton<AppCore>
{
    [SerializeField]
    private FadingUI _loadingScreen;
    [SerializeField]
    private SceneManagement _scenes;

    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private float _totalSceneProgress;

    private int _currentMapIndex = -1;
    private int _currentDoorIndex = -1;
    private bool _isTeleporting = false;

    void Start()
    {
        MainMenu();
    }

    private IEnumerator wrapLoading(Action actions)
    {
        //blockPlayerInput(true);
        yield return _loadingScreen.show();
        //Do actions between loading screens
        actions();
        yield return GetSceneLoadProgress();
        //blockPlayerInput(false);
        yield return _loadingScreen.hide();
    }

    private IEnumerator wrapLoadingAwait(Action actions)
    {
        //blockPlayerInput(true);
        yield return _loadingScreen.show();
        //Do actions between loading screens
        actions();
        yield return GetSceneLoadProgress();
        //Break this loading by calling
        //AppCore.Instance?.LoadingDone();
    }

    private IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < _scenesLoading.Count; i++)
        {
            while (!_scenesLoading[i].isDone)
            {
                _totalSceneProgress = 0;

                foreach (AsyncOperation operation in _scenesLoading)
                {
                    _totalSceneProgress += operation.progress;
                }

                _totalSceneProgress = (_totalSceneProgress / _scenesLoading.Count) * 100f;

                Debug.Log("Progress: " + _totalSceneProgress);
                //bar.current = Mathf.RoundToInt(totalSceneProgress);

                yield return null;
            }
        }
        _scenesLoading.Clear();
    }

    public void LoadingDone()
    {
        StartCoroutine(loadingDone());
    }

    private IEnumerator loadingDone()
    {
        //blockPlayerInput(false);
        yield return _loadingScreen.hide();
    }

    public void MainMenu()
    {
        StartCoroutine(wrapLoading(() =>
        {
            _scenesLoading.Add(SceneManager.LoadSceneAsync(_scenes.GetMainMenuIndex(), LoadSceneMode.Additive));
        }));
    }

    public void StartGame()
    {
        _isTeleporting = true;
        _currentDoorIndex = -1;
        StartCoroutine(wrapLoadingAwait(() =>
        {
            //Resources.Load<CharacterData>("Character Data").ResetData();
            _scenesLoading.Add(SceneManager.UnloadSceneAsync(_scenes.GetMainMenuIndex()));
            _scenes.GetGameplayScenes().ForEach(scene =>
            {
                _scenesLoading.Add(SceneManager.LoadSceneAsync(scene.BuildIndex, LoadSceneMode.Additive));
            });
            _currentMapIndex = _scenes.GetFirstRoomIndex();
            _scenesLoading.Add(SceneManager.LoadSceneAsync(_currentMapIndex, LoadSceneMode.Additive));
        }));
    }

    public void MoveToRoom(MapScenes levelDestination, int doorIndex)
    {
        _isTeleporting = false;
        _currentDoorIndex = doorIndex;
        StartCoroutine(wrapLoadingAwait(() =>
        {
            _scenesLoading.Add(SceneManager.UnloadSceneAsync(_currentMapIndex));
            _currentMapIndex = _scenes.getMapSceneIndexByType(levelDestination);
            _scenesLoading.Add(SceneManager.LoadSceneAsync(_currentMapIndex, LoadSceneMode.Additive));
        }));
    }

    public void TeleportToRoom(MapScenes levelDestination)
    {
        _isTeleporting = true;
        _currentDoorIndex = -1;
        StartCoroutine(wrapLoadingAwait(() =>
        {
            _scenesLoading.Add(SceneManager.UnloadSceneAsync(_currentMapIndex));
            _currentMapIndex = _scenes.getMapSceneIndexByType(levelDestination);
            _scenesLoading.Add(SceneManager.LoadSceneAsync(_currentMapIndex, LoadSceneMode.Additive));
        }));
    }

    public int GetLastDoorIndex()
    {
        return _currentDoorIndex;
    }

    public bool IsTeleporting()
    {
        return _isTeleporting;
    }
}