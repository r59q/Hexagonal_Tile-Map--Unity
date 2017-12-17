using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    public class Tile
    {
        GameObject gameObject;

        TileData data;
        TileBehaviour behaviour;

        Vector2 index;

        Vector3 position;
        float height;

        MapGenerator generator;

        public Tile(TileData tiledata,Vector3 pos, float heightOffset, MapGenerator mapGenerator, Vector2 gridIndex)
        {
            GrabVariables(tiledata,pos,heightOffset,mapGenerator,gridIndex);
        }

        // Relative height to grid space
        public float Height()
        {
            return height;
        }
        public void Height(float newHeight)
        {
            // set new height
            height = newHeight;

            // change position
            position = new Vector3(
                generator.GetPos(index).x,
                generator.GetPos(index).y + height,
                generator.GetPos(index).z);

            // update object's position
            gameObject.transform.position = position;
        }

        // relative height to world space
        public float WorldHeight()
        {
            return gameObject.transform.position.y;
        }
        public void WorldHeight(float newHeight)
        {
            // Grab the original height
            float originalHeight = GameManager.instance.transform.position.y;
            
            // set position
            gameObject.transform.position = position = new Vector3(
                position.x,
                newHeight,
                position.z);

            // grab new height and determine what our relative height should be.
            float nHeight = WorldHeight();
            height = originalHeight + nHeight;

        }

        // Set and get tile
        public TileData GetTileData()
        {
            return data;
        }
        public void SetTileData(TileData newData)
        {
            if (gameObject == null)
            {
                data = newData;
            }
            else
            {
                Debug.Log("<color=olive>Warning! Could not set new TileData</color>\n" +
                    "Tile already exists and is instantiated. There is no point");
            }
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public GameObject Instantiate()
        {

            if (gameObject == null)
            {
                gameObject = GameObject.Instantiate(data.prefab, position, Quaternion.Euler(Vector3.zero));

                behaviour = gameObject.GetComponent<TileBehaviour>();

                if (behaviour == null)
                {
                    Debug.Log("<color=blue>Notice: Could not find TileBehaviour</color>\n" +
                        "Have you forgotten to add one on the prefab?");
                }
                else
                {
                    behaviour.Initialize(data, this);
                }
            }
            else
            {
                Debug.Log("<color=olive>Warning! Could not spawn tile</color>\n"+
                    "Tile already exists and is instantiated");
            }
            return gameObject;
        }

        public Tile[] Neighbours ()
        {
            List<Tile> result = new List<Tile>();

            // same row
            result = AddNeighbour(generator.Tile((int)index.x - 1, (int)index.y), result);
            result = AddNeighbour(generator.Tile((int)(index.x + 1), (int)index.y), result);

            if (index.y % 2 == 0)
            {
                // Upper row
                result = AddNeighbour(generator.Tile((int)(index.x - 1), (int)(index.y - 1)), result);
                result = AddNeighbour(generator.Tile((int)(index.x), (int)(index.y - 1)), result);

                // lower row
                result = AddNeighbour(generator.Tile((int)(index.x - 1), (int)(index.y + 1)), result);
                result = AddNeighbour(generator.Tile((int)(index.x), (int)(index.y + 1)), result);
            }
            else
            {
                // Upper row
                result = AddNeighbour(generator.Tile((int)(index.x + 1), (int)(index.y - 1)), result);
                result = AddNeighbour(generator.Tile((int)(index.x), (int)(index.y - 1)), result);

                // lower row
                result = AddNeighbour(generator.Tile((int)(index.x + 1), (int)(index.y + 1)), result);
                result = AddNeighbour(generator.Tile((int)(index.x), (int)(index.y + 1)), result);

            }

            return result.ToArray();
        }

        List<Tile> AddNeighbour(Tile neighbour,List<Tile> list)
        {
            if (neighbour != null) { 
                list.Add(neighbour);
            }
            return list;
        }

        //For local use
        void GrabVariables(TileData tiledata, Vector3 pos, float heightOffset, MapGenerator mapGenerator, Vector2 gridIndex)
        {
            // Initialize and grab all the needed variables
            data = tiledata;
            position = pos;
            height = heightOffset;
            generator = mapGenerator;
            index = gridIndex;
            position = new Vector3(
                position.x,
                position.y + height,
                position.z);
        }
    }
}