using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap { 
    public class GameManager : MonoBehaviour {

        public static GameManager instance = null;

        MapGenerator mapHandler;

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
            mapHandler = GetComponent<MapGenerator>();
            if (mapHandler == null)
            {
                throw new System.Exception("No map generator found!\nOne must be added to the 'GameManager' object!");
            }
            // generate level
            mapHandler.Build();
        }

        public MapGenerator MapHandler()
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