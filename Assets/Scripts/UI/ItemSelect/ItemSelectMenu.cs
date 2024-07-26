namespace NUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
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
        private GameObject foreGround;
        [SerializeField]
        private GameObject backGround;
        [SerializeField]
        private CanvasGroup canvasGroup;

        private float fadeTime = 1.0f;

        public void OnClickTrash()
        {
            SoundManager.instance.PlaySe("button");
            FadeOut();
        }

        public void OnClickEquip1()
        {
            SoundManager.instance.PlaySe("button");
            PlayerPrefs.SetInt("NowEquip1", newEquip.GetComponent<SelectEquipmentContainer>().GetItem().equipmentID);
            FadeOut();
        }

        public void OnClickEquip2()
        {
            SoundManager.instance.PlaySe("button");
            PlayerPrefs.SetInt("NowEquip2", newEquip.GetComponent<SelectEquipmentContainer>().GetItem().equipmentID);
            FadeOut();
        }

        public void OnClickEquip3()
        {
            SoundManager.instance.PlaySe("button");
            PlayerPrefs.SetInt("NowEquip3", newEquip.GetComponent<SelectEquipmentContainer>().GetItem().equipmentID);
            FadeOut();
        }

        private void FadeIn()
        {
            foreGround.GetComponent<Image>().DOFade(1.0f, fadeTime).OnComplete(() =>
            {
                Color c = backGround.GetComponent<Image>().color;
                backGround.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1.0f);
                canvasGroup.alpha = 1.0f;
                foreGround.GetComponent<Image>().DOFade(0.0f, fadeTime).OnComplete(() =>
                {
                    foreGround.SetActive(false);
                });
            });
        }

        private void FadeOut()
        {
            foreGround.SetActive(true);
            foreGround.GetComponent<Image>().DOFade(1.0f, fadeTime).OnComplete(() =>
            {
                Color c = backGround.GetComponent<Image>().color;
                backGround.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0.0f);
                canvasGroup.alpha = 0.0f;
                GameManager.instance.SetUp();

                foreGround.GetComponent<Image>().DOFade(0.0f, fadeTime).OnComplete(() =>
                {
                    foreGround.SetActive(false);

                    if (SceneManager.GetSceneByName("ItemScene").isLoaded)
                    {
                        SceneManager.UnloadSceneAsync("ItemScene");
                    }
                });
            });
        }

        private void Start()
        {
            Cursor.visible = true;
            int newEquipID = GameManager.instance.RandomRange(1, allEquipments.list.Count);

            nowEquip1.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip1", 0)]);
            nowEquip2.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip2", 1)]);
            nowEquip3.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip3", 2)]);
            newEquip.GetComponent<SelectEquipmentContainer>().SetItem(allEquipments.list[newEquipID]);

            FadeIn();
        }
    }
}
