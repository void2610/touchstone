namespace NManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using NBless;

    public class BlessManager : MonoBehaviour
    {
        [SerializeField]
        private BlessDataList allBlessDataList;
        private List<BlessData> allBlessData => allBlessDataList.list;

        private List<GameObject> currentBlessObj = new List<GameObject>();
        private List<BlessBase> currentBless = new List<BlessBase>();
        private List<Vector3> currentBlessPos = new List<Vector3>();
        private GameObject blessContainer;
        private GameObject player;

        private float radius = 5.0f;

        public void GetRandomBless()
        {
            GameObject newBless = null;
            float randomValue = Random.Range(0f, 1f);
            float sum = 0;
            foreach (var blessData in allBlessData)
            {
                sum += blessData.blessProbability;
                if (randomValue <= sum)
                {
                    Debug.Log(blessData.blessName);
                    newBless = Instantiate(blessData.blessPrefab, blessContainer.transform);
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

            currentBlessPos.Add(bestPosition);
            newBless.GetComponent<BlessBase>().SetBasePosition(bestPosition, player);
        }

        void Awake()
        {
            blessContainer = new GameObject("BlessContainer");
        }

        void Start()
        {
            player = GameManager.instance.playerObj;
            GetRandomBless();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetRandomBless();
            }
        }
    }
}
