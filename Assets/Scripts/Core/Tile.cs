using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    /// <summary>
    /// Contains the data associated with a tile. Such as position, GameObject, and more.
    /// </summary>
    public class Tile
    {
        // The data about the instantiated object
        TileData data;
        GameObject gameObject;
        Vector3 position;
        TileBehaviour[] behaviours;

        // Data internal to the tile generation system
        Vector2 index;
        MapGenerator generator;
        float height;

        /// <summary>
        /// Create a tile class instance. Contains methods such as
        /// setting the height or instantiating the GameObject.
        /// Creating a tile instance does NOT instantiate a GameObject.
        /// </summary>
        /// <param name="tiledata">The associated tile data.</param>
        /// <param name="pos">The world space position.</param>
        /// <param name="heightOffset">The height offset. Internal to the MapGenerator</param>
        /// <param name="mapGenerator">The MapGenerator you wish associated with this tile.</param>
        /// <param name="gridIndex">The grid index you wish this tile to reside on.</param>
        public Tile(TileData tiledata,Vector3 pos, float heightOffset, MapGenerator mapGenerator, Vector2 gridIndex)
        {
            if (mapGenerator == null)
            {
                throw new System.Exception("The MapGenerator:MapGenerator cannot be null !");
            }
            // Check if index is out of bounds.
            if ((gridIndex.x >= mapGenerator.tileMap.size.x || gridIndex.x < 0)
                || (gridIndex.y >= mapGenerator.tileMap.size.y || gridIndex.y < 0))
            {
                throw new System.Exception("The Vector2:GridIndex is out of bounds! Please generate tiles within map borders");
            }

            // Set local variables from given parameters.
            GrabVariables(tiledata,pos,heightOffset,mapGenerator,gridIndex);
        }

        /// <summary>
        /// Returns the height offset relative to the map grid.
        /// </summary>
        /// <returns>Height offset relative to map grid</returns>
        public float Height()
        {
            return height;
        }

        /// <summary>
        /// Sets the height offset of the tile. If the gameobject has 
        /// already been spawned, also reposition it.
        /// </summary>
        /// <param name="newHeight">The new height offset</param>
        public void Height(float newHeight)
        {
            if (gameObject == null)
            {
                // set new height
                height = newHeight;

                // change position
                position = new Vector3(
                    generator.tileMap.GetWorldPos(index).x,
                    generator.tileMap.GetWorldPos(index).y + height,
                    generator.tileMap.GetWorldPos(index).z);
            }
            else
            {
                // set new height
                height = newHeight;

                // change position
                position = new Vector3(
                    generator.tileMap.GetWorldPos(index).x,
                    generator.tileMap.GetWorldPos(index).y + height,
                    generator.tileMap.GetWorldPos(index).z);

                // update object's position
                gameObject.transform.position = position;
            }
        }

        /// <summary>
        /// Returns the tile's height in world space.
        /// </summary>
        /// <returns>Height in world space.</returns>
        public float WorldHeight()
        {
            return gameObject.transform.position.y;
        }

        /// <summary>
        /// Set new world space height for the tile.
        /// </summary>
        /// <param name="newHeight">New world space height.</param>
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

        /// <summary>
        /// Returns the TileData asset associated with this tile.
        /// </summary>
        /// <returns>The TileData asset of this tile.</returns>
        public TileData GetTileData()
        {
            return data;
        }

        /// <summary>
        /// Sets a new TileData. If it has already been spawned, 
        /// also respawn it in order for it to change.
        /// </summary>
        /// <param name="newData">the new TileData.</param>
        public void SetTileData(TileData newData)
        {
            if (gameObject == null)
            {
                data = newData;
            }
            else
            {
                Destroy();
                data = newData;
                gameObject = Instantiate();
            }
        }

        /// <summary>
        /// Destroys the GameObject associated with this tile.
        /// </summary>
        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }

        /// <summary>
        /// Destroys the GameObject associated with this tile after 
        /// a selected period of time.
        /// </summary>
        /// <param name="time">Time before GameObject is destroyed.</param>
        public void Destroy(float time)
        {
            GameObject.Destroy(gameObject,time);
        }

        /// <summary>
        /// Get the grid index of this tile.
        /// </summary>
        /// <returns>The grid index.</returns>
        public Vector2 Index()
        {
            return index;
        }

        /// <summary>
        /// Returns the MapGenerator associated with this tile.
        /// </summary>
        /// <returns>The MapGenerator of this tile.</returns>
        public MapGenerator Generator()
        {
            return generator;
        }

        /// <summary>
        /// Sets the MapGenerator of this tile. Do not touch if you do not know what you are doing!
        /// </summary>
        /// <param name="newGenerator">The new MapGenerator.</param>
        public void Generator(MapGenerator newGenerator)
        {
            generator = newGenerator;
        }

        /// <summary>
        /// Returns the instantiated GameObject associated with this tile. 
        /// Returns null if none have been instantiated.
        /// </summary>
        /// <returns>The GameObject of this tile.</returns>
        public GameObject GetGameObject()
        {
            return gameObject;
        }

        /// <summary>
        /// Instantiates a GameObject in the scene at the correct location.
        /// To do this a tile MUST have a TileData asset associated with it.
        /// It also initializes any TileBehaviour scripts on it.
        /// </summary>
        /// <returns>The instantiated GameObject.</returns>
        public GameObject Instantiate()
        {
            if (gameObject == null)
            {
                if (data != null)
                {
                    gameObject = GameObject.Instantiate(data.prefab, position, Quaternion.Euler(Vector3.zero));

                    if ((behaviours = gameObject.GetComponents<TileBehaviour>()).Length > 0)
                    {
                        foreach (TileBehaviour behaviour in behaviours)
                        {
                            behaviour.Initialize(this);
                        }
                    }
                }
                else
                {
                    Debug.Log("<color=olive>Warning! Could not find TileData</color>\n" +
                    "Has it been set?");
                }
            }
            else
            {
                Debug.Log("<color=olive>Warning! Could not spawn tile</color>\n"+
                    "Tile already exists and is instantiated");
            }
            return gameObject;
        }

        /// <summary>
        /// Returns all of the Tiles, that are touching sides with this tile in an array.
        /// </summary>
        /// <returns>Tiles, that are touching sides with this tile</returns>
        public Tile[] Neighbours ()
        {
            List<Tile> result = new List<Tile>();

            // same row
            result = AddNeighbour(generator.tileMap.Tile((int)index.x - 1, (int)index.y), result);
            result = AddNeighbour(generator.tileMap.Tile((int)(index.x + 1), (int)index.y), result);

            if (index.y % 2 == 0)
            {
                // Upper row
                result = AddNeighbour(generator.tileMap.Tile((int)(index.x - 1), (int)(index.y - 1)), result);
                result = AddNeighbour(generator.tileMap.Tile((int)(index.x), (int)(index.y - 1)), result);

                // lower row
                result = AddNeighbour(generator.tileMap.Tile((int)(index.x - 1), (int)(index.y + 1)), result);
                result = AddNeighbour(generator.tileMap.Tile((int)(index.x), (int)(index.y + 1)), result);
            }
            else
            {
                // Upper row
                result = AddNeighbour(generator.tileMap.Tile((int)(index.x + 1), (int)(index.y - 1)), result);
                result = AddNeighbour(generator.tileMap.Tile((int)(index.x), (int)(index.y - 1)), result);

                // lower row
                result = AddNeighbour(generator.tileMap.Tile((int)(index.x + 1), (int)(index.y + 1)), result);
                result = AddNeighbour(generator.tileMap.Tile((int)(index.x), (int)(index.y + 1)), result);

            }

            return result.ToArray();
        }

        // Methods for shortening code.
        List<Tile> AddNeighbour(Tile neighbour,List<Tile> list)
        {
            if (neighbour != null) { 
                list.Add(neighbour);
            }
            return list;
        }

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