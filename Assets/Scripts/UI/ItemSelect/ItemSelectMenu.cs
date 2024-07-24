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
            Initiate.Fade("MainScene", Color.black, 1.0f);
        }

        public void OnClickEquip1()
        {
            PlayerPrefs.SetInt("NowEquip1", newEquip.GetComponent<SelectEquipmentContainer>().GetItem().equipmentID);
            Initiate.Fade("MainScene", Color.black, 1.0f);
        }

        public void OnClickEquip2()
        {
            PlayerPrefs.SetInt("NowEquip2", newEquip.GetComponent<SelectEquipmentContainer>().GetItem().equipmentID);
            Initiate.Fade("MainScene", Color.black, 1.0f);
        }

        public void OnClickEquip3()
        {
            PlayerPrefs.SetInt("NowEquip3", newEquip.GetComponent<SelectEquipmentContainer>().GetItem().equipmentID);
            Initiate.Fade("MainScene", Color.black, 1.0f);
        }

        void Start()
        {
            Cursor.visible = true;

            nowEquip1.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip1", 0)]);
            nowEquip2.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip2", 1)]);
            nowEquip3.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip3", 2)]);
            newEquip.GetComponent<SelectEquipmentContainer>().SetItem(allEquipments.list[GameManager.instance.RandomRange(0, allEquipments.list.Count)]);

            //背景色の透明度を0から1に変化させる
            backGround.GetComponent<Image>().DOFade(1.0f, 2.0f).OnComplete(() =>
            {
                //キャンバスグループの透明度を1に変化させる
                canvasGroup.alpha = 1.0f;
            });
        }

        void Update()
        {

        }
    }
}
