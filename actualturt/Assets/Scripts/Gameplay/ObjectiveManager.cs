using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    public int shellsRequiredToUnlockNext = 10;
    public int islandIndexToUnlock = 1; // example

    void Awake() { Instance = this; }

    public void NotifyCollectiblePicked(int value)
    {
        CheckProgression();
    }

    void CheckProgression()
    {
        var prog = GameManager.Instance.Progression;
        // simple example: if shells >= required, unlock island 2
        if (!prog.islandUnlocked[islandIndexToUnlock] && prog.shellsCollected >= shellsRequiredToUnlockNext)
        {
            prog.islandUnlocked[islandIndexToUnlock] = true;
            SaveManager.SaveProgress(prog);
            // Visual feedback: notify hub gates
            foreach (var gate in FindObjectsOfType<IslandGate>()) gate.Refresh();
            // toast message etc.
            Debug.Log("Unlocked island index " + islandIndexToUnlock);
        }
    }
}
