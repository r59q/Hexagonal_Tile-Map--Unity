using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.TileBehaviours
{
    public class RandomColor : TileBehaviour
    {

        protected override void OnInitialize()
        {
            GetComponent<MeshRenderer>().material.color = new Color(
                Random.Range(0f,1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f));
                
        }

        protected override void OnUpdate()
        {

        }
    }
}