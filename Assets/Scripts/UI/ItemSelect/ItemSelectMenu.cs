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
        private int newEquipID = 0;

        public void OnClickTrash()
        {
            SoundManager.instance.PlaySe("button");
            FadeOut();
        }

        public void OnClickEquip1()
        {
            SoundManager.instance.PlaySe("button");
            PlayerPrefs.SetInt("NowEquip1", newEquipID);
            GameManager.instance.player.Heal(1);
            FadeOut();
        }

        public void OnClickEquip2()
        {
            SoundManager.instance.PlaySe("button");
            PlayerPrefs.SetInt("NowEquip2", newEquipID);
            GameManager.instance.player.Heal(1);
            FadeOut();
        }

        public void OnClickEquip3()
        {
            SoundManager.instance.PlaySe("button");
            PlayerPrefs.SetInt("NowEquip3", newEquipID);
            GameManager.instance.player.Heal(1);
            FadeOut();
        }

        private void FadeIn()
        {
            if (foreGround == null && backGround == null) return;

            foreGround.GetComponent<Image>().DOFade(1.0f, fadeTime).OnComplete(() =>
            {
                if (foreGround == null && backGround == null) return;
                Color c = backGround.GetComponent<Image>().color;
                backGround.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1.0f);
                canvasGroup.alpha = 1.0f;
                canvasGroup.interactable = true;
                BGMManager.instance.EnableLowPassFilter();
                foreGround.GetComponent<Image>().DOFade(0.0f, fadeTime).OnComplete(() =>
                {
                    if (foreGround == null && backGround == null) return;
                    foreGround.SetActive(false);
                });
            });
        }

        private void FadeOut()
        {
            if (foreGround == null && backGround == null) return;

            foreGround.SetActive(true);
            foreGround.GetComponent<Image>().DOFade(1.0f, fadeTime).OnComplete(() =>
            {
                if (foreGround == null && backGround == null) return;
                Color c = backGround.GetComponent<Image>().color;
                backGround.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0.0f);
                canvasGroup.alpha = 0.0f;
                canvasGroup.interactable = false;
                GameManager.instance.SetUp();
                BGMManager.instance.DisableLowPassFilter();

                foreGround.GetComponent<Image>().DOFade(0.0f, fadeTime).OnComplete(() =>
                {
                    if (foreGround == null && backGround == null) return;
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
            newEquipID = GameManager.instance.RandomRange(1, allEquipments.list.Count);
            while (allEquipments.list[newEquipID].equipmentName == "Heal")
            {
                newEquipID = GameManager.instance.RandomRange(1, allEquipments.list.Count);
            }

            nowEquip1.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip1", 1)]);
            nowEquip2.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip2", 0)]);
            nowEquip3.GetComponent<EquipmentContainer>().SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip3", 0)]);
            newEquip.GetComponent<SelectEquipmentContainer>().SetItem(allEquipments.list[newEquipID]);

            FadeIn();
        }
    }
}
