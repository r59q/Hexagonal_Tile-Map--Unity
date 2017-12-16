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

        Vector2 index;

        MapGenerator generator;

        float height;

        public Tile(TileData tiledata,Vector3 pos, float heightOffset, MapGenerator mapGenerator, Vector2 gridIndex)
        {
            data = tiledata;
            position = pos;
            height = heightOffset;
            generator = mapGenerator;
            index = gridIndex;


            position = new Vector3(
                position.x,
                position.y + height,
                position.z);

            gameObject = GameObject.Instantiate(tiledata.prefab, position, Quaternion.Euler(Vector3.zero));

            behaviour = gameObject.GetComponent<TileBehaviour>();
            behaviour.Initialize(data,this);

            if (behaviour == null)
            {
                Debug.Log("<color=olive>Warning! Could not find TileBehaviour</color>\n"+
                    "Have you forgotten to add one on the prefab?");
            }
        }

        public float Height()
        {
            return height;
        }

        

        public void Height(float newHeight)
        {

            height = newHeight;

            /*
            position = new Vector3(
                generator.GetPos(this).x,
                generator.GetPos(this).y + height,
                generator.GetPos(this).z);
            BAD PERFORMANCE, BUT POSSIBLE
            */

            position = new Vector3(
                generator.GetPos(index).x,
                generator.GetPos(index).y + height,
                generator.GetPos(index).z);

            gameObject.transform.position = position;
        }

        public float WorldHeight()
        {
            return gameObject.transform.position.y;
        }

        public void WorldHeight(float newHeight)
        {
            float originalHeight = GameManager.instance.transform.position.y;

            gameObject.transform.position = position = new Vector3(
                position.x,
                newHeight,
                position.z);

            float nHeight = WorldHeight();
            height = originalHeight + nHeight;

        }

    }
}