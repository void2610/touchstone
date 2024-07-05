namespace NTitle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NEquipment;


    public class EquipmentContainerList : MonoBehaviour
    {
        [SerializeField]
        private TitleMenu titleMenu;
        [SerializeField]
        private EquipmentDataList equipmentDataList;
        [SerializeField]
        private GameObject equipmentContainerPrefab;

        [SerializeField]
        private float alignX = 1;
        [SerializeField]
        private float alignY = 1;
        [SerializeField]
        private int column = 3;
        [SerializeField]
        private float containerSize = 1;

        private void Start()
        {
            float adjustedAlignX = alignX * (Screen.width / 1920f);
            float adjustedAlignY = alignY * (Screen.height / 1080f);

            for (int i = equipmentDataList.list.Count - 1; i >= 0; i--)
            {
                Vector3 pos = new Vector3((i % column) * adjustedAlignX, -(i / column) * adjustedAlignY, 0);
                GameObject container = Instantiate(equipmentContainerPrefab, this.transform.position + pos, Quaternion.identity, this.transform);
                container.transform.localScale = new Vector3(containerSize, containerSize, containerSize);
                container.GetComponent<TitleEquipmentContainer>().SetItem(equipmentDataList.list[i]);
                int temp = i;
                container.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (PlayerPrefs.GetInt("Equip" + equipmentDataList.list[temp].equipmentID, 0) == 1)
                    {
                        titleMenu.OnClickEquipButton(temp);
                    }
                    else
                    {
                        container.GetComponent<TitleEquipmentContainer>().BuyItem();
                    }
                });
            }
        }
    }

}
