using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelDataStructure : ScriptableObject
{
    public int level;
    public Vector3 playerSpawnPosition;
    public float levelAltitude;
}
