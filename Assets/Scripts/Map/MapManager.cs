namespace NMap
{
    using UnityEngine;
    using UnityEngine.Tilemaps;
    using System.Collections;
    using System.Collections.Generic;
    using NManager;


    public class MapManager : MonoBehaviour
    {
        private Transform player;
        private float maxHight = 0;
        void Start()
        {
            player = GameManager.instance.player.transform;
        }

        void Update()
        {
            maxHight = Mathf.Max(maxHight, player.position.y);
        }
    }
}
