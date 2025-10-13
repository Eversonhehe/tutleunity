using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public ProgressionData Progression = new ProgressionData();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SaveManager.LoadProgress(Progression); // attempt load on start
    }

    void OnApplicationQuit()
    {
        SaveManager.SaveProgress(Progression);
    }
}
