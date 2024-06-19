namespace NMap
{
    using UnityEngine;
    using UnityEngine.Tilemaps;
    using System.Collections;
    using System.Collections.Generic;
    using NManager;


    public class MapManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> mapPrefabs;
        [SerializeField]
        private bool isRandomedSeed = false;
        [SerializeField]
        private int seed = 42;
        private GameObject mapContainer;
        private Transform player;
        private float pAltitude = 0;
        private int nextHight = 0;
        private int mapHight = 40;

        private void SetMap()
        {
            int index = Random.Range(0, mapPrefabs.Count);
            GameObject map = Instantiate(mapPrefabs[index], new Vector3(0, nextHight, 0), Quaternion.identity);
            map.transform.SetParent(mapContainer.transform);
            nextHight += mapHight;
        }

        void Start()
        {
            mapContainer = new GameObject("MapContainer");
            player = GameManager.instance.player.transform;
            if (isRandomedSeed)
            {
                seed = System.DateTime.Now.Millisecond;
            }
            Random.InitState(seed);
        }

        void Update()
        {
            pAltitude = Mathf.Max(pAltitude, player.position.y);

            if (pAltitude + 20 > nextHight)
            {
                SetMap();
            }
        }
    }
}