namespace NMap
{
    using UnityEngine;
    using UnityEngine.Tilemaps;
    using System.Collections;
    using System.Collections.Generic;
    using NManager;


    public class MapManager : MonoBehaviour
    {
        [System.Serializable]
        public class MapTileData
        {
            public GameObject prefab;
            public int difficulty;
        }


        [Header("Map Tiles")]
        [SerializeField]
        private GameObject firstMap;
        [SerializeField]
        private GameObject goalPrefab;
        [SerializeField]
        private GameObject goalBackGroundPrefab;
        [SerializeField]
        private List<MapTileData> mapTiles;

        [Header("Map Settings")]
        [SerializeField]
        private List<int> mapLengths;
        [SerializeField]
        private int termialMapLength = 10;
        [SerializeField]
        private int mapHight = 40;
        [SerializeField]
        private float startHight = 80;

        public float mapEndAltitude { get; private set; }
        private GameObject mapContainer;
        private Transform player;

        private int stageCount = 0;
        private float nextHight = 0;
        private int mapCount = 0;

        private List<int> mapSeedList = new List<int>();

        private System.Random currentRandom;

        public void SetUp(bool proceed = true)
        {
            if (proceed) stageCount++;

            if (stageCount >= GameManager.instance.StageSize)
            {
                GameManager.instance.GameClear();
                return;
            }
            currentRandom = new System.Random(GameManager.instance.mapRandomSeeds[stageCount]);
            Debug.Log("Seed: " + currentRandom.Next() + " " + stageCount);
            int mapLength = stageCount < mapLengths.Count ? mapLengths[stageCount] : termialMapLength;
            mapEndAltitude = (mapLength) * mapHight;

            if (mapContainer != null)
            {
                Destroy(mapContainer);
            }
            if (firstMap.transform.Find("EnemyContainer") != null)
            {
                Destroy(firstMap.transform.Find("EnemyContainer").gameObject);
            }
            if (firstMap.transform.Find("ObjectContainer") != null)
            {
                Destroy(firstMap.transform.Find("ObjectContainer").gameObject);
            }

            nextHight = startHight;
            mapContainer = new GameObject("MapContainer");
            player = GameManager.instance.player.transform;
            for (int i = 0; i < mapLength - 1; i++)
            {
                SetMap(mapHight);
            }
            Instantiate(goalPrefab, new Vector3(0, mapEndAltitude, 0), Quaternion.identity, mapContainer.transform);
            Instantiate(goalBackGroundPrefab, new Vector3(0, nextHight, 0), Quaternion.identity, mapContainer.transform);
            firstMap.GetComponent<MapCreater>().Create(currentRandom);
        }

        private void SetMap(float h)
        {
            // mapCountが増えるにつれて難易度が高いマップが選ばれやすくなる
            float difficultyModifier = ((mapCount + 1) * 0.1f);

            float totalAdjustedDifficulty = 0;
            foreach (var tile in mapTiles)
            {
                if (tile.difficulty >= 100) Debug.LogError("Difficulty is over 100");
                totalAdjustedDifficulty += (100 - tile.difficulty) * difficultyModifier;
            }
            float randomValue = RandomRange(0, totalAdjustedDifficulty);

            foreach (var tile in mapTiles)
            {
                randomValue -= (100 - tile.difficulty) * difficultyModifier;
                if (randomValue <= 0)
                {
                    var m = Instantiate(tile.prefab, new Vector3(0, nextHight, 0), Quaternion.identity, mapContainer.transform);
                    m.GetComponent<MapCreater>().Create(currentRandom);
                    break;
                }
            }

            nextHight += h;
            mapCount++;
        }

        private float RandomRange(float min, float max)
        {
            float randomValue = (float)(this.currentRandom.NextDouble() * (max - min) + min);
            return randomValue;
        }

        private int RandomRange(int min, int max)
        {
            int randomValue = this.currentRandom.Next(min, max);
            return randomValue;
        }

        private void Update()
        {
            if (!Application.isEditor) return;
            if (Input.GetKeyDown(KeyCode.G))
            {
                GameManager.instance.playerObj.transform.position = new Vector3(0, mapEndAltitude + 5, 0);
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                GameManager.instance.player.Heal(1);
            }
        }
    }
}
