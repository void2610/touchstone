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
        protected override void Awake()
        {
            base.Awake();
            equipmentName = "Foothold";
            coolTimeLength = 30f;
            activeTimeLength = 10f;
        }

        protected override void OnActionStart()
        {
            base.OnActionStart();
            GameObject foothold = Instantiate(footholdPrefab, player.transform.position + Vector3.down * 3, Quaternion.Euler(0, 0, 90));
            Destroy(foothold, activeTimeLength);
        }
    }
}
