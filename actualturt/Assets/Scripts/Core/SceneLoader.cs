using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    public CanvasGroup fadeCanvas;
    public float fadeTime = 0.6f;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadCoroutine(sceneName));
    }

    IEnumerator LoadCoroutine(string sceneName)
    {
        // fade out
        yield return StartCoroutine(Fade(1));
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        // do not allow activation until fully loaded if you want to wait for preloads
        while (!op.isDone) yield return null;
        // fade in
        yield return StartCoroutine(Fade(0));
    }

    IEnumerator Fade(float targetAlpha)
    {
        if (fadeCanvas == null) yield break;
        float start = fadeCanvas.alpha;
        float t = 0;
        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            fadeCanvas.alpha = Mathf.Lerp(start, targetAlpha, t / fadeTime);
            yield return null;
        }
        fadeCanvas.alpha = targetAlpha;
    }
}
