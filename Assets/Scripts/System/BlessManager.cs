namespace NManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using NBless;
    using NCharacter;

    public class BlessManager : MonoBehaviour
    {
        private struct BlessPosData
        {
            public BlessBase bless;
            public Vector3 position;
        }

        [SerializeField]
        private BlessDataList allBlessDataList;

        private List<BlessData> allBlessData => allBlessDataList.list;
        private List<GameObject> currentBlessObj = new List<GameObject>();
        private List<BlessPosData> currentBless = new List<BlessPosData>();
        private GameObject blessContainer;
        private Player player;

        private float radius = 4.0f;

        //ダメージを受けたときにBlessの効果を発動し、trueならダメージを無効化
        public bool OnPlayerDamaged()
        {
            foreach (var b in currentBless)
            {
                if (b.bless.OnPlayerDamaged(player))
                {
                    RemoveBless(b.bless);
                    return true;
                }
            }
            return false;
        }

        public bool OnPlayerCantJumped()
        {
            foreach (var b in currentBless)
            {
                if (b.bless.OnPlayerCantJumped(player))
                {
                    RemoveBless(b.bless);
                    return true;
                }
            }
            return false;
        }

        public void GetRandomBless(Vector3 spawnPos = default)
        {
            SoundManager.instance.PlaySe("bless");
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
                foreach (var b in currentBless)
                {
                    float distanceToP = Vector3.Distance(b.position, pos);
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
            currentBless.Add(new BlessPosData { bless = newBless.GetComponent<BlessBase>(), position = bestPosition });
            newBless.GetComponent<BlessBase>().SetBasePosition(bestPosition, GameManager.instance.playerObj);

            if (newBless.GetComponent<BlessBase>().OnActive(player))
            {
                RemoveBless(newBless.GetComponent<BlessBase>());
            }
        }

        private void RemoveBless(BlessBase b)
        {
            int index = 0;
            foreach (var bless in currentBless)
            {
                if (bless.bless == b)
                {
                    break;
                }
                index++;
            }
            currentBless[index].bless.OnDeactive(player);
            currentBless[index].bless.PlayDisapearParticle();
            currentBless.RemoveAt(index);
            Destroy(currentBlessObj[index]);
            currentBlessObj.RemoveAt(index);
        }
        void Awake()
        {
            blessContainer = new GameObject("BlessContainer");
        }

        void Update()
        {
            if (!Application.isEditor) return;

            if (Input.GetKeyDown(KeyCode.B))
            {
                GetRandomBless();
            }
        }
    }
}
