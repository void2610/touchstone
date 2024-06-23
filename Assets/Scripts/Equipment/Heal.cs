namespace NEquipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NCharacter;

    public class Heal : Equipment
    {
        protected override void Awake()
        {
            base.Awake();
            equipmentName = "Heal";
            coolTimeLength = 20f;
            activeTimeLength = 0.15f;
        }

        protected override void OnActionStart()
        {
            base.OnActionStart();
            player.GetComponent<PlayerParticles>().PlayHealParticle();
            player.GetComponent<Player>().Heal(1);
        }
    }
}
