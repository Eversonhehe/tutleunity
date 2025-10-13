using UnityEngine;
using UnityEngine.UI;

public class IslandGate : MonoBehaviour
{
    public int islandIndex = 0; // 0,1,2
    public string sceneName;
    public GameObject lockedVisual; // e.g. chains, dark overlay
    public Button gateButton; // optional UI button near gate

    void Start()
    {
        Refresh();
        if (gateButton) gateButton.onClick.AddListener(OnUseGate);
    }

    public void Refresh()
    {
        bool unlocked = GameManager.Instance.Progression.islandUnlocked[islandIndex];
        if (lockedVisual) lockedVisual.SetActive(!unlocked);
        if (gateButton) gateButton.interactable = unlocked;
    }

    public void OnUseGate()
    {
        bool unlocked = GameManager.Instance.Progression.islandUnlocked[islandIndex];
        if (!unlocked)
        {
            // show "locked" feedback (e.g. play sound, popup)
            Debug.Log("Island locked!");
            return;
        }

        // Optionally save last hub position
        GameManager.Instance.Progression.lastScene = sceneName;
        SaveManager.SaveProgress(GameManager.Instance.Progression);

        SceneLoader.Instance.LoadScene(sceneName);
    }
}
