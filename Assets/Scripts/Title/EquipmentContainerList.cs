namespace NTitle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using NEquipment;

    public class EquipmentContainerList : MonoBehaviour
    {
        [SerializeField]
        private EquipmentDataList equipmentDataList;
        [SerializeField]
        private GameObject equipmentContainerPrefab;

        [SerializeField]
        private float containerAligment = 100f;
        [SerializeField]
        private int row = 3;
        [SerializeField]
        private float containerSize = 1;

        private void SetAllItems()
        {
            for (int i = 0; i < equipmentDataList.list.Count; i++)
            {
                Vector3 pos = new Vector3((i % row) * containerAligment, (i / row) * -containerAligment, 0);
                GameObject container = Instantiate(equipmentContainerPrefab, this.transform.position + pos, Quaternion.identity, this.transform);
                container.transform.localScale = new Vector3(containerSize, containerSize, containerSize);
                container.GetComponent<EquipmentContainer>().SetItem(equipmentDataList.list[i]);
            }
        }

        private void Start()
        {
            SetAllItems();
        }
    }

}
