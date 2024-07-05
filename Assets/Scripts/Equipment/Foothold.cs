namespace NEquipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NCharacter;

    public class Foothold : Equipment
    {
        [SerializeField]
        private GameObject footholdPrefab;
        private GameObject footholdInstance;

        private void PlayParticleSystem()
        {
            footholdInstance.GetComponent<ParticleSystem>().Play();
        }

        protected override void Awake()
        {
            base.Awake();
            equipmentName = "Foothold";
            coolTimeLength = 20f;
            activeTimeLength = 10f;
            isHold = false;
        }

        protected override void OnActionStart()
        {
            base.OnActionStart();
            footholdInstance = Instantiate(footholdPrefab, player.transform.position + Vector3.down * 3, Quaternion.Euler(0, 0, 90));
            Invoke("PlayParticleSystem", activeTimeLength - 1f);
            Destroy(footholdInstance, activeTimeLength);
        }

        protected override void OnActionEnd()
        {
            base.OnActionEnd();
        }
    }
}
