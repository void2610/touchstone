namespace NMap
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;
    using NManager;

    public class MapCreater : MonoBehaviour
    {
        [System.Serializable]
        internal class PrefabWithWeight
        {
            public GameObject prefab;
            public float weight;
        }

        [SerializeField]
        private Vector2Int mapSize;
        [SerializeField]
        private float offset;
        [Header("Object")]
        [SerializeField]
        private int objectNum;
        [SerializeField]
        private List<PrefabWithWeight> objects;
        [Header("Enemy")]
        [SerializeField]
        private int enemyNum;

        [SerializeField]
        private List<PrefabWithWeight> enemies;

        [Header("Setting")]
        [SerializeField]
        private bool createOnStart = false;
        public void Create()
        {
            var objectContainer = new GameObject("ObjectContainer");
            objectContainer.transform.parent = this.transform;
            List<Vector3> objectPositions = new List<Vector3>();

            for (int i = 0; i < objectNum; i++)
            {
                Vector3 position = GetFarPosition(objectPositions);
                float totalWeight = objects.Sum(o => o.weight);
                float randomValue = GameManager.instance.RandomRange(0, totalWeight);

                foreach (var o in objects)
                {
                    randomValue -= o.weight;
                    if (randomValue <= 0)
                    {
                        Instantiate(o.prefab, position, Quaternion.identity, objectContainer.transform);
                        objectPositions.Add(position);
                        break;
                    }
                }
            }

            var enemyContainer = new GameObject("EnemyContainer");
            enemyContainer.transform.parent = this.transform;
            List<Vector3> enemyPositions = new List<Vector3>();

            for (int i = 0; i < enemyNum; i++)
            {
                Vector3 position = GetFarPosition(objectPositions.Concat(enemyPositions).ToList());
                float totalWeight = enemies.Sum(e => e.weight);
                float randomValue = GameManager.instance.RandomRange(0, totalWeight);

                foreach (var e in enemies)
                {
                    randomValue -= e.weight;
                    if (randomValue <= 0)
                    {
                        Instantiate(e.prefab, position, Quaternion.identity, enemyContainer.transform);
                        enemyPositions.Add(position);
                        break;
                    }
                }
            }
        }

        private Vector3 GetFarPosition(List<Vector3> existingPositions)
        {
            Vector3 bestPosition = Vector3.zero;
            float bestDistance = 0;

            for (int attempt = 0; attempt < 10; attempt++) // 最大10回の試行
            {
                int x = GameManager.instance.RandomRange(0, mapSize.x);
                int y = GameManager.instance.RandomRange(0, mapSize.y);
                Vector3 position = this.transform.position + new Vector3(x - mapSize.x / 2, y - mapSize.y / 2 + offset, 0);

                float minDistance = existingPositions.Count == 0 ? float.MaxValue : existingPositions.Min(p => Vector3.Distance(p, position));

                if (minDistance > bestDistance)
                {
                    bestDistance = minDistance;
                    bestPosition = position;
                }
            }

            return bestPosition;
        }

        private void Start()
        {
            if (createOnStart)
            {
                Create();
            }
        }
    }
}
