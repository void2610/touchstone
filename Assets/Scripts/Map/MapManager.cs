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
        private int mapLength = 5;
        [SerializeField]
        private int mapHight = 40;
        [SerializeField]
        private float startHight = 80;
        [SerializeField]
        private List<GameObject> mapPrefabs;
        [SerializeField]
        private GameObject goalPrefab;

        public float mapEndAltitude => (mapCount + 1) * mapHight;
        private GameObject mapContainer;
        private Transform player;

        private float nextHight = 0;
        private int mapCount = 0;


        private void SetMap(float h)
        {
            int index = Random.Range(0, mapPrefabs.Count);
            var m = Instantiate(mapPrefabs[index], new Vector3(0, nextHight, 0), Quaternion.identity, mapContainer.transform);
            m.GetComponent<MapCreater>().Create();
            nextHight += h;
            mapCount++;
        }

        void Start()
        {
            nextHight = startHight;
            mapContainer = new GameObject("MapContainer");
            player = GameManager.instance.player.transform;
            for (int i = 0; i < mapLength; i++)
            {
                SetMap(mapHight);
            }
            GameObject map = Instantiate(mapPrefabs[0], new Vector3(0, nextHight, 0), Quaternion.identity, mapContainer.transform);
            Instantiate(goalPrefab, new Vector3(0, mapEndAltitude, 0), Quaternion.identity, mapContainer.transform);
        }

        void Update()
        {
        }
    }
}
