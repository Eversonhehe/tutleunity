using UnityEngine;

public class HubManager : MonoBehaviour
{
    void Start()
    {
        // When the Hub loads, refresh all gates
        RefreshAllGates();
    }

    public static void RefreshAllGates()
    {
        IslandGate[] gates = FindObjectsOfType<IslandGate>();
        foreach (var gate in gates)
            gate.Refresh();
    }
}
