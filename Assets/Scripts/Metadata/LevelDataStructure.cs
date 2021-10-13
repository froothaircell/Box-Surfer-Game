using UnityEngine;

/// <summary>
/// A scriptable object to store the static data for a level
/// </summary>
[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelDataStructure : ScriptableObject
{
    public int level;
    public Vector3 playerSpawnPosition;
    public float levelAltitude;
}
