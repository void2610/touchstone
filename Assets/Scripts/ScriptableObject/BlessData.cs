namespace NBless
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "BlessData", menuName = "Scriptable Objects/BlessData")]
    public class BlessData : ScriptableObject
    {
        public int blessID;
        public string blessName;
        public string blessDescription;
        public float blessProbability;
        public GameObject blessPrefab;
    }
}
