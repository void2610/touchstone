namespace NEquipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Wepon : Equipment
    {
        public int attackPower;

        public void ResetActionTime()
        {
            isActive = true;
        }

        public virtual void Action()
        {
            base.Action();
        }

        public virtual void Start()
        {
            base.Start();
            name = "Wepon";
            actionKey = "Mouse0";
            coolTimeLength = 0.1f;
            isEnable = true;
        }

        public virtual void Update()
        {
            base.Update();
        }

        public virtual void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
