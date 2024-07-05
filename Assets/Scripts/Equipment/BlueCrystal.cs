namespace NEquipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NCharacter;
    using NInterface;

    public class BlueCrystal : Equipment, IEquipmentModifiable
    {
        protected override void Awake()
        {
            base.Awake();
            equipmentName = "BlueCrystal";
            coolTimeLength = 999f;
            activeTimeLength = 999f;
            isHold = false;
            isEnable = false;
        }

        public void ModifyEquipment(Equipment e1, Equipment e2, Equipment e3)
        {
            e1.coolTimeLength /= 2f;
            e2.coolTimeLength /= 2f;
            e3.coolTimeLength /= 2f;
        }
    }
}
