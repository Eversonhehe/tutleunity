[System.Serializable]
public class ProgressionData
{
    // index 0 = Island1, 1 = Island2, 2 = Island3
    public bool[] islandUnlocked = new bool[3] { true, false, false }; // allow first island optionally
    public string lastScene = "Hub";
    // add upgrades/unlocks as needed
    public int shellsCollected = 0;
}
