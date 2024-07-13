namespace NMap
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;

    public class MapCreater : MonoBehaviour
    {
        [System.Serializable]
        internal class EnemyPrefabWithWeight
        {
            public GameObject prefab;
            public float weight;
        }

        [SerializeField]
        private Vector2Int mapSize;
        [SerializeField]
        private int enemyNum;
        [SerializeField]
        private List<EnemyPrefabWithWeight> enemies;

        void Start()
        {
            for (int i = 0; i < enemyNum; i++)
            {
                int x = Random.Range(0, mapSize.x);
                int y = Random.Range(0, mapSize.y);

                // 重みを元に敵を選択
                float totalWeight = enemies.Sum(enemy => enemy.weight);
                float randomValue = Random.Range(0, totalWeight);
                foreach (var enemy in enemies)
                {
                    randomValue -= enemy.weight;
                    if (randomValue <= 0)
                    {
                        Instantiate(enemy.prefab, this.transform.position + new Vector3(x - mapSize.x / 2, y - mapSize.y / 2, 0), Quaternion.identity);
                        break;
                    }
                }
            }
        }
    }
}
