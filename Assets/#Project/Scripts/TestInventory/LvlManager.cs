using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    public GameObject musicPrefab;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(musicPrefab);
    }

}
