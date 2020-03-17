## Loading screen 

* load scene by index `LoadingScreen.instance.Load( int );`
* load scene by name `LoadingScreen.instance.Load( string );`
* check progress `var p = LoadingScreen.instance.progress_value;`
* note loading will not start if another scene is still loading, check via `bool b = LoadingScreen.instance.isLoading;`
* stop current loading scene: `LoadingScreen.instance.Stop( () => { Debug.Log("stop complete"); } );`

```cs
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    static public LoadingScreen instance;

    [SerializeField] GameObject loadingScreenOverlay;

    public bool isLoading { get; private set; } = false;

    public float progressValue { get; private set; }
    public string progressMessage { get; private set; }

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad( gameObject );

        loadingScreenOverlay.SetActive(false);
    }

    public void Load(int sceneIndex)
    {
        var op = SceneManager.LoadSceneAsync(sceneIndex);
        StartCoroutine(LoadAsynchronously(op));
    }

    public void Load(string sceneName)
    {
        if( isLoading ) return;

        isLoading = true;

        var op = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(LoadAsynchronously(op));
    }

    IEnumerator LoadAsynchronously(AsyncOperation op)
    {
        loadingScreenOverlay.SetActive(true);

        while ( ! op.isDone )
        {
            float loaded_value = Mathf.Clamp01(op.progress / 0.95f);
            progressMessage = $"{(loaded_value * 100).ToString("N1")} %";
            progressValue = loaded_value;

            if( halt )
            {
                halt = false;
                onStop?.Invoke();
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        isLoading = false;
    }

    System.Action onStop;
    bool halt = false;

    public void Stop( System.Action onStop = null )
    {
        halt = true;
        isLoading = false;
        this.onStop = onStop;
    }
}
```
