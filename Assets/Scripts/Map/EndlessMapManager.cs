namespace NMap
{
    using UnityEngine;
    using UnityEngine.Tilemaps;
    using System.Collections;
    using System.Collections.Generic;
    using NManager;


    public class EndlessMapManager : MonoBehaviour
    {
        [SerializeField]
        private float mapClearHight = 100;
        [SerializeField]
        private int mapHight = 40;
        [SerializeField]
        private float startHight = 80;
        [SerializeField]
        private List<GameObject> mapPrefabs;
        private GameObject mapContainer;
        private Transform player;

        private float pAltitude = 0;
        private float nextHight = 0;
        private int clearIndex = 0;

        private void SetMap()
        {
            int index = GameManager.instance.RandomRange(0, mapPrefabs.Count);
            GameObject map = Instantiate(mapPrefabs[index], new Vector3(0, nextHight, 0), Quaternion.identity, mapContainer.transform);
            map.GetComponent<MapCreater>().Create();
            nextHight += mapHight;
        }

        void Start()
        {
            nextHight = startHight;
            mapContainer = new GameObject("MapContainer");
            player = GameManager.instance.player.transform;
        }

        void Update()
        {
            pAltitude = Mathf.Max(pAltitude, player.position.y);

            if (pAltitude + 60 > nextHight)
            {
                SetMap();
            }

            if (pAltitude > mapClearHight + clearIndex * mapHight)
            {
                Destroy(mapContainer.transform.GetChild(0).gameObject);
                clearIndex++;
            }
        }
    }
}
