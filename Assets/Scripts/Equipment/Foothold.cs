namespace NEquipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NCharacter;
    using NManager;

    public class Foothold : Equipment
    {
        [SerializeField]
        private GameObject footholdPrefab;
        private GameObject footholdInstance;

        private void PlayParticleSystem()
        {
            footholdInstance.GetComponentInChildren<ParticleSystem>().Play();
        }

        protected override void Awake()
        {
            base.Awake();
            equipmentName = "Foothold";
            coolTimeLength = 5f;
            activeTimeLength = 2f;
            isHold = false;
        }

        protected override void OnActionStart()
        {
            base.OnActionStart();
            footholdInstance = Instantiate(footholdPrefab, player.transform.position + Vector3.down * 3, Quaternion.identity);
            footholdInstance.transform.localScale *= intensity;
            Invoke("PlayParticleSystem", activeTimeLength - 1f);
            Destroy(footholdInstance, activeTimeLength);
            SoundManager.instance.PlaySe("foothold");
        }

        protected override void OnActionEnd()
        {
            base.OnActionEnd();
        }
    }
}
