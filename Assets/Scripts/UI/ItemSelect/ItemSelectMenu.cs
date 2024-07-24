namespace NUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using NEquipment;
    using NManager;
    using DG.Tweening;

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
        [SerializeField]
        private GameObject newEquip;
        [SerializeField]
        private GameObject backGround;
        [SerializeField]
        private CanvasGroup canvasGroup;

        public void OnClickTrash()
        {
            SoundManager.instance.PlaySe("button");
            GameManager.instance.SetUp();
        }

        public void OnClickEquip1()
        {
            SoundManager.instance.PlaySe("button");
            PlayerPrefs.SetInt("NowEquip1", newEquip.GetComponent<SelectEquipmentContainer>().GetItem().equipmentID);
            GameManager.instance.SetUp();
        }

        public void OnClickEquip2()
        {
            SoundManager.instance.PlaySe("button");
            PlayerPrefs.SetInt("NowEquip2", newEquip.GetComponent<SelectEquipmentContainer>().GetItem().equipmentID);
            GameManager.instance.SetUp();
        }

        public void OnClickEquip3()
        {
            SoundManager.instance.PlaySe("button");
            PlayerPrefs.SetInt("NowEquip3", newEquip.GetComponent<SelectEquipmentContainer>().GetItem().equipmentID);
            GameManager.instance.SetUp();
        }

        void Start()
        {
            Cursor.visible = true;
            int newEquipID = GameManager.instance.RandomRange(0, allEquipments.list.Count);

            nowEquip1.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip1", 0)]);
            nowEquip2.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip2", 1)]);
            nowEquip3.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip3", 2)]);
            newEquip.GetComponent<SelectEquipmentContainer>().SetItem(allEquipments.list[newEquipID]);

            backGround.GetComponent<Image>().DOFade(1.0f, 2.0f).OnComplete(() =>
            {
                canvasGroup.alpha = 1.0f;
            });
        }
    }
}
