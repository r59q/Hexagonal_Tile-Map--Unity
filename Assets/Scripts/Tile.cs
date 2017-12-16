using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    public class Tile
    {
        TileData data;
        TileBehaviour behaviour;

        GameObject gameObject;

        Vector3 position;

        public Tile(TileData tiledata,Vector3 pos, float heightOffset)
        {
            data = tiledata;
            position = pos;

            position = new Vector3(
                position.x,
                position.y + heightOffset,
                position.z);

            gameObject = GameObject.Instantiate(tiledata.prefab, position, Quaternion.Euler(Vector3.zero));

            behaviour = gameObject.GetComponent<TileBehaviour>();
            behaviour.Initialize(data);

            if (behaviour == null)
            {
                Debug.Log("<color=olive>Warning! Could not find TileBehaviour</color>\n"+
                    "Have you forgotten to add one on the prefab?");
            }
        }

    }
}