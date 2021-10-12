using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMetaData : MonoBehaviour
{
    [SerializeField]
    private LevelDataStructure levelInfo;

    public LevelDataStructure LevelInfo
    {
        get
        {
            return levelInfo;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
