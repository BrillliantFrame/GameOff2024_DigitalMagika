using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene Manager", menuName = "ScriptableObjects/Core Objects/New Scene Manager", order = 1)]
public class SceneManagement : ScriptableObject
{
    [SerializeField]
    private List<UISceneItem> _userInterfaceScenes = new List<UISceneItem>();
    [SerializeField]
    private List<GameplaySceneItem> _gameplayScenes = new List<GameplaySceneItem>();
    [SerializeField]
    private List<MapSceneItem> _mapScenes = new List<MapSceneItem>();

    private int GetUISceneIndexByType(UIScenes sceneType)
    {
        UISceneItem scene = _userInterfaceScenes.FirstOrDefault(scene => scene.Scene == sceneType);
        if (scene != null)
            return scene.BuildIndex;
        return -1;
    }

    public int getMapSceneIndexByType(MapScenes sceneType)
    {
        MapSceneItem scene = _mapScenes.FirstOrDefault(scene => scene.Scene == sceneType);
        if (scene != null)
            return scene.BuildIndex;
        return -1;
    }

    public List<GameplaySceneItem> GetGameplayScenes()
    {
        return _gameplayScenes;
    }

    public int GetMainMenuIndex()
    {
        return GetUISceneIndexByType(UIScenes.MainMenu);
    }

    public int GetFirstRoomIndex()
    {
        //return getMapSceneIndexByType(MapScenes.I_BasicMovementTut);
        return getMapSceneIndexByType(MapScenes.X_A_MonolithRoom);
    }

    public int GetCreditsIndex()
    {
        return GetUISceneIndexByType(UIScenes.Credits);
    }
}

[Serializable]
public class BaseSceneItem
{
    public int BuildIndex = 0;
}

[Serializable]
public class GameplaySceneItem : BaseSceneItem
{
    public string SceneName;
}

[Serializable]
public class MapSceneItem : BaseSceneItem
{
    public MapScenes Scene;
}

[Serializable]
public class UISceneItem : BaseSceneItem
{
    public UIScenes Scene;
}