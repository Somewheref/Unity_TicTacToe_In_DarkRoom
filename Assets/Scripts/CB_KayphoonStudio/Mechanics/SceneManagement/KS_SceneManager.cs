using UnityEngine;
using UnityEngine.SceneManagement;
using KayphoonStudio.UI;
using System.Collections;
using KayphoonStudio;
using System;
using System.Collections.Generic;


public class KS_SceneManager : MonoBehaviour
{
    public static KS_SceneManager Instance { get; private set; }
    public static bool initialized = false;
    private static bool sceneSwitchedFirstTime = true;

    private const string LoadingSceneName = "LoadingScene";  // Adjust this if your loading scene has a different name.

    private string targetSceneToLoadByName;
    private int? targetSceneToLoadByIndex;
    private bool isLoadingScene = false;
    private static string _async_lastSceneName;    // used to unload the current scene after the loading scene is loaded.

    public static Action onSceneLoaded; // called everytime a scene is loaded.
    public static Action onSceneSwitched;   // called everytime a scene is loaded, but exclude the first time the game starts.

    public static string CurrentSceneName
    {
        get
        {
            //SetupInstanceIfNeeded();
            return SceneManager.GetActiveScene().name;
        }
    }

    private void Awake()
    {
        // Ensure only one instance of this manager exists.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        if (!initialized)
        {
            initialized = true;

            _async_lastSceneName = CurrentSceneName;

            onSceneSwitched += () => { OnSceneSwitched();};
            onSceneSwitched += onSceneLoaded;

            SceneManager.sceneLoaded += (scene, mode) => {
                if (sceneSwitchedFirstTime)
                    sceneSwitchedFirstTime = false;
                else
                    onSceneSwitched?.Invoke(); };
        }

        // move this object to the root of the hierarchy so it persists between scene loads.
        // transform.SetParent(null);

        // DontDestroyOnLoad(gameObject);
    }

    IEnumerator Start() {
        yield return new WaitForEndOfFrame();
        onSceneLoaded?.Invoke();
    }

    void OnSceneSwitched()
    {
        KS_Logger.Log("Scene switched: " + CurrentSceneName, this);
        KS_Timer.DelayedEndOfFrameAction(() => {
            KS_Logger.Log("Scene switched notification dispatched in scene: " + CurrentSceneName, this);
            KS_NotificationCenter.DispatchEvent(KS_InternalNotificationKeys.OnSceneSwitched, CurrentSceneName);
            });
    }

    public static void LoadScene(string targetSceneName)
    {
        SetupInstanceIfNeeded();

        if (Instance.isLoadingScene)
        {
            KS_Logger.LogWarning("Cannot load scene " + targetSceneName + " because another scene is already loading.", Instance);
            return;
        }

        SceneManager.LoadScene(targetSceneName);
        _async_lastSceneName = targetSceneName;

        //onSceneSwitched?.Invoke();
    }

    public static void LoadScene(int targetSceneIndex)
    {
        SetupInstanceIfNeeded();

        if (Instance.isLoadingScene)
        {
            KS_Logger.LogWarning("Cannot load scene " + targetSceneIndex + " because another scene is already loading.", Instance);
            return;
        }

        SceneManager.LoadScene(targetSceneIndex);
        _async_lastSceneName = SceneManager.GetSceneByBuildIndex(targetSceneIndex).name;

        //onSceneSwitched?.Invoke();
    }

    public static void LoadSceneAsync(string targetSceneName)
    {
        SetupInstanceIfNeeded();

        if (Instance.isLoadingScene)
        {
            KS_Logger.LogWarning("Cannot load scene " + targetSceneName + " because another scene is already loading.", Instance);
            return;
        }

        Instance.targetSceneToLoadByName = targetSceneName;
        Instance.targetSceneToLoadByIndex = null;

        LoadLoadingScene();
    }

    public static void LoadSceneAsync(int targetSceneIndex)
    {
        SetupInstanceIfNeeded();

        if (Instance.isLoadingScene)
        {
            KS_Logger.LogWarning("Cannot load scene " + targetSceneIndex + " because another scene is already loading.", Instance);
            return;
        }

        Instance.targetSceneToLoadByName = null;
        Instance.targetSceneToLoadByIndex = targetSceneIndex;

        LoadLoadingScene();
    }

    public static void ReloadActiveScene()
    {
        SetupInstanceIfNeeded();

        if (Instance.isLoadingScene)
        {
            KS_Logger.LogWarning("Cannot reload scene because another scene is already loading.", Instance);
            return;
        }

        LoadScene(CurrentSceneName);
    }

    private static void SetupInstanceIfNeeded()
    {
        if (Instance == null)
        {
            var managerGameObject = new GameObject("KS_SceneManager");
            managerGameObject.AddComponent<KS_SceneManager>();
        }
    }

    private static void LoadLoadingScene()
    {
        Instance.isLoadingScene = true;
        KS_Logger.Log("KS_SceneManager: " + LoadingSceneName + " unloaded successfully.");
        SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive).completed += Instance.OnLoadingSceneLoaded;
    }

    private void OnLoadingSceneLoaded(AsyncOperation obj)
    {
        StartCoroutine(PreLoadTargetSceneAsync());
    }

    private IEnumerator PreLoadTargetSceneAsync()
    {
        // Ensure that the loading scene has loaded and the instance of KS_UI_LoadingScreen_AsyncScene is available.
        while (KS_UI_LoadingScreen_AsyncScene.Instance == null)
        {
            yield return null;
        }

        KS_UI_LoadingScreen_AsyncScene.Instance.FadeIn(onComplete: () => 
        {
            StartCoroutine(FullyLoadTargetSceneAsync());
        });
        
    }

    private IEnumerator FullyLoadTargetSceneAsync()
    {
        // Unload the current scene.
        // patch fix 2023.12.20: upon first start up the _async_lastSceneName may have weird bug. Try catching the error here.
        try
        {
            SceneManager.UnloadSceneAsync(_async_lastSceneName);
        }
        catch (Exception e)
        {
            KS_Logger.LogError("Error unloading scene: " + _async_lastSceneName + ". Error: " + e.Message, this);
            SceneManager.UnloadSceneAsync(GetActiveSceneExceptLoading()[0].name);

            KS_Logger.Log("Patch fix worked!");
        }

        _async_lastSceneName = LoadingSceneName;
        Scene targetScene = default(Scene);

        AsyncOperation asyncLoad;
        if (targetSceneToLoadByIndex.HasValue)
        {
            targetScene = SceneManager.GetSceneByBuildIndex(targetSceneToLoadByIndex.Value);
            asyncLoad = SceneManager.LoadSceneAsync(targetSceneToLoadByIndex.Value, LoadSceneMode.Additive);
        }
        else
        {
            targetScene = SceneManager.GetSceneByName(targetSceneToLoadByName);
            asyncLoad = SceneManager.LoadSceneAsync(targetSceneToLoadByName, LoadSceneMode.Additive);
        }

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;

                //SceneManager.SetActiveScene(targetScene);

                KS_UI_LoadingScreen_AsyncScene.Instance.FadeOut(onComplete: () => {SceneManager.UnloadSceneAsync(LoadingSceneName); onSceneSwitched?.Invoke();});

                //KS_Logger.Log(LoadingSceneName + " unloaded successfully.", this);

                _async_lastSceneName = targetSceneToLoadByName ?? SceneManager.GetSceneByBuildIndex(targetSceneToLoadByIndex.Value).name;

                isLoadingScene = false;
            }

            yield return null;
        }
    }


    private List<Scene> GetActiveSceneExceptLoading()
    {
        List<Scene> activeScene = new List<Scene>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != LoadingSceneName)
            {
                activeScene.Add(scene);
            }
        }
        return activeScene;
    }


    private void OnDestroy()
    {
        Instance = null;
    }
}

