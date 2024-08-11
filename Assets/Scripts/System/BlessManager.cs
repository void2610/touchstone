namespace NManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using NBless;
    using NCharacter;

    public class BlessManager : MonoBehaviour
    {
        [SerializeField]
        private BlessDataList allBlessDataList;
        private List<BlessData> allBlessData => allBlessDataList.list;

        private List<GameObject> currentBlessObj = new List<GameObject>();
        private List<BlessBase> currentBless = new List<BlessBase>();
        private List<Vector3> currentBlessPos = new List<Vector3>();
        private GameObject blessContainer;
        private Player player;

        private float radius = 5.0f;

        //ダメージを受けたときにBlessの効果を発動し、trueならダメージを無効化
        public bool OnPlayerDamaged()
        {
            foreach (var bless in currentBless)
            {
                if (bless.OnPlayerDamaged(player))
                {
                    RemoveBless(currentBless.IndexOf(bless));
                    return true;
                }
            }
            return false;
        }

        public void GetRandomBless(Vector3 spawnPos = default)
        {
            player = GameManager.instance.player;
            GameObject newBless = null;
            float total = 0;
            foreach (var blessData in allBlessData)
            {
                total += blessData.blessProbability;
            }
            float randomValue = Random.Range(0f, total);
            float sum = 0;
            foreach (var blessData in allBlessData)
            {
                sum += blessData.blessProbability;
                if (randomValue <= sum)
                {
                    newBless = Instantiate(blessData.blessPrefab, spawnPos, Quaternion.identity, blessContainer.transform);
                    currentBlessObj.Add(newBless);
                    currentBless.Add(newBless.GetComponent<BlessBase>());
                    break;
                }
            }

            //radiusの範囲内にランダムに配置
            //既存のBlessとの距離ができるだけ離れるように配置(10回まで)
            Vector3 bestPosition = Vector3.zero;
            float bestDistance = 0;

            for (int attempt = 0; attempt < 10; attempt++)
            {
                float angle = Random.Range(0f, Mathf.PI * 2);
                float distance = Random.Range(0f, radius);
                Vector3 pos = new Vector3(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance, 0);

                float minDistance = float.MaxValue;
                foreach (var p in currentBlessPos)
                {
                    float distanceToP = Vector3.Distance(p, pos);
                    if (distanceToP < minDistance)
                    {
                        minDistance = distanceToP;
                    }
                }

                if (minDistance > bestDistance && Vector3.Distance(pos, Vector3.zero) >= 2f)
                {
                    bestDistance = minDistance;
                    bestPosition = pos;
                }
            }
            bestPosition.z = -10;
            currentBlessPos.Add(bestPosition);
            newBless.GetComponent<BlessBase>().SetBasePosition(bestPosition, GameManager.instance.playerObj);

            if (newBless.GetComponent<BlessBase>().OnActive(player))
            {
                RemoveBless(currentBless.IndexOf(newBless.GetComponent<BlessBase>()));
            }
        }

        private void RemoveBless(int index)
        {
            currentBless[index].OnDeactive(player);
            currentBless[index].PlayDisapearParticle();
            currentBless.RemoveAt(index);
            currentBlessPos.RemoveAt(index);
            Destroy(currentBlessObj[index]);
            currentBlessObj.RemoveAt(index);
        }

        void Awake()
        {
            blessContainer = new GameObject("BlessContainer");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                GetRandomBless();
            }
        }
    }
}
