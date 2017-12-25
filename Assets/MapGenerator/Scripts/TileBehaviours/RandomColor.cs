using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.TileBehaviours
{
    public class RandomColor : TileBehaviour
    {

        float cooldown;
        float timer;

        private void Start()
        {
            cooldown = Random.Range(.5f, 5);

            // Random height
            tile.Height(Random.Range(0f, 2f));
        }

        private void Update()
        {
            timer -= Time.deltaTime;
        }

        private void FixedUpdate()
        {

            if (timer < 0) {
                cooldown = Random.Range(.5f, 5);

                GetComponent<MeshRenderer>().material.color = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f));

                if (Random.Range(0f,1f) > 0.5f)
                {
                    tile.Height(Random.Range(0f, 2f));
                }

                timer = cooldown;
            }
        }

    }
}