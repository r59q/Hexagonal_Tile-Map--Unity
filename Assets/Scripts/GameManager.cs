using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap { 
    public class GameManager : MonoBehaviour {

        public static GameManager instance = null;

        MapHandler mapHandler;

	    // Use this for initialization
	    void Awake () {
            // Making singleton
            if (!CreateSingleton())
                Destroy(gameObject);
            else
                DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            // Initialize below
            mapHandler = GetComponent<MapHandler>();
            mapHandler.BuildMap();
        }

        public MapHandler MapHandler()
        {
            return mapHandler;
        }

        bool CreateSingleton()
        {
            if (instance == null)
            {
                instance = this;
                return true;
            }
            return false;
        }
    }
}