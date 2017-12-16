using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    MapHandler mapHandler;

	// Use this for initialization
	void Awake () {
        mapHandler = GetComponent<MapHandler>();     
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        mapHandler.BuildMap();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
