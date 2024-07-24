namespace NUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using NEquipment;

    public class ItemSelectMenu : MonoBehaviour
    {
        [SerializeField]
        private EquipmentDataList allEquipments;
        [SerializeField]
        private GameObject nowEquip1;
        [SerializeField]
        private GameObject nowEquip2;
        [SerializeField]
        private GameObject nowEquip3;
        void Start()
        {
            nowEquip1.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip1", 0)]);
            nowEquip2.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip2", 1)]);
            nowEquip3.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip3", 2)]);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
