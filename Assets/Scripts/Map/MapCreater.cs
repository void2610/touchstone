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
            for (int i = 0; i < objectNum; i++)
            {
                int x = GameManager.instance.RandomRange(0, mapSize.x);
                int y = GameManager.instance.RandomRange(0, mapSize.y);

                // 重みを元に敵を選択
                float totalWeight = objects.Sum(o => o.weight);
                float randomValue = GameManager.instance.RandomRange(0, totalWeight);
                foreach (var o in objects)
                {
                    randomValue -= o.weight;
                    if (randomValue <= 0)
                    {
                        Instantiate(o.prefab, this.transform.position + new Vector3(x - mapSize.x / 2, y - mapSize.y / 2 + offset, 0), Quaternion.identity, objectContainer.transform);
                        break;
                    }
                }
            }


            var enemyContainer = new GameObject("EnemyContainer");
            enemyContainer.transform.parent = this.transform;
            for (int i = 0; i < enemyNum; i++)
            {
                int x = GameManager.instance.RandomRange(0, mapSize.x);
                int y = GameManager.instance.RandomRange(0, mapSize.y);

                // 重みを元に敵を選択
                float totalWeight = enemies.Sum(enemy => enemy.weight);
                float randomValue = GameManager.instance.RandomRange(0, totalWeight);
                foreach (var enemy in enemies)
                {
                    randomValue -= enemy.weight;
                    if (randomValue <= 0)
                    {
                        Instantiate(enemy.prefab, this.transform.position + new Vector3(x - mapSize.x / 2, y - mapSize.y / 2 + offset, 0), Quaternion.identity, enemyContainer.transform);
                        break;
                    }
                }
            }
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
