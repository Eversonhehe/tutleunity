using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1;
    public AudioClip pickupSfx;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.Progression.shellsCollected += value;
        SaveManager.SaveProgress(GameManager.Instance.Progression);
        // Play SFX, particles
        Destroy(gameObject);
        ObjectiveManager.Instance.NotifyCollectiblePicked(value);
    }
}
