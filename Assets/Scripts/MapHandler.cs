using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour {

    public GameObject tilePrefab;

    public Vector2 mapSize;
    public float sideWidth;

    float tileWidth;
    float tileHeight;

    float constant = 0.866f;

    private void Start()
    {
        tileHeight = 2 * sideWidth;
        tileWidth = 2 * (sideWidth * constant);
    }

    public void BuildMap()
    {

        for (int i = 0; i < mapSize.x; i++)
        {
            for (int k = 0; k < mapSize.y; k++)
            {

                Vector2 pos = new Vector2(i, k);

                Instantiate( tilePrefab , GetPos(pos) , Quaternion.Euler(Vector3.zero) );

                print(i + "-" + k);

            }
        }

    }

    Vector3 GetPos(Vector2 pos)
    {

        Vector3 originalPos = transform.position;

        float offset;

        if (pos.y % 2 == 0)
        {
            offset = tileWidth / 2;
        } else
        {
            offset = 0;
        }


        Vector3 result = new Vector3((originalPos.x + ((tileWidth)*pos.x ))+offset, originalPos.y, originalPos.z + (((sideWidth /2)*3)*pos.y));

        return result;

    }

}
