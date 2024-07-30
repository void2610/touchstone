namespace NEquipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NCharacter;
    using NManager;

    public class Heal : Equipment
    {
        protected override void Awake()
        {
            base.Awake();
            equipmentName = "Heal";
            coolTimeLength = 20f;
            activeTimeLength = 0.15f;
            isHold = false;
        }

        protected override void OnActionStart()
        {
            base.OnActionStart();
            player.GetComponent<PlayerParticles>().PlayHealParticle();
            player.GetComponent<Player>().Heal(1);
            SoundManager.instance.PlaySe("heal");
        }
    }
}
