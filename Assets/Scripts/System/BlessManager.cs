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
        private GameObject blessContainer;

        public void GetABless()
        {
            float randomValue = Random.Range(0f, 1f);
            float sum = 0;
            foreach (var blessData in allBlessData)
            {
                sum += blessData.blessProbability;
                if (randomValue <= sum)
                {
                    Debug.Log(blessData.blessName);
                    var b = Instantiate(blessData.blessPrefab, blessContainer.transform);
                    currentBlessObj.Add(b);
                    currentBless.Add(b.GetComponent<BlessBase>());
                    break;
                }
            }
        }

        void Awake()
        {
            blessContainer = new GameObject("BlessContainer");
        }

        void Start()
        {
            GetABless();
        }

        void Update()
        {

        }
    }
}
