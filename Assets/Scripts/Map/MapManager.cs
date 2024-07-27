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
        private GameObject firstMap;
        [SerializeField]
        private GameObject goalPrefab;
        [SerializeField]
        private GameObject goalBlockGroundPrefab;

        public float mapEndAltitude;
        private GameObject mapContainer;
        private Transform player;

        private float nextHight = 0;
        private int mapCount = 0;

        public void SetUp()
        {
            if (mapLength <= 0) return;
            mapEndAltitude = (mapLength) * mapHight;

            if (mapContainer != null)
            {
                Destroy(mapContainer);
            }
            if (firstMap.transform.Find("EnemyContainer") != null)
            {
                Destroy(firstMap.transform.Find("EnemyContainer").gameObject);
            }

            nextHight = startHight;
            mapContainer = new GameObject("MapContainer");
            player = GameManager.instance.player.transform;
            for (int i = 0; i < mapLength - 1; i++)
            {
                SetMap(mapHight);
            }
            Instantiate(goalPrefab, new Vector3(0, mapEndAltitude, 0), Quaternion.identity, mapContainer.transform);
            Instantiate(goalBlockGroundPrefab, new Vector3(0, nextHight, 0), Quaternion.identity, mapContainer.transform);
            firstMap.GetComponent<MapCreater>().Create();
        }

        private void SetMap(float h)
        {
            int index = GameManager.instance.RandomRange(0, mapPrefabs.Count);
            var m = Instantiate(mapPrefabs[0], new Vector3(0, nextHight, 0), Quaternion.identity, mapContainer.transform);
            m.GetComponent<MapCreater>().Create();

            nextHight += h;
            mapCount++;
        }
    }
}
