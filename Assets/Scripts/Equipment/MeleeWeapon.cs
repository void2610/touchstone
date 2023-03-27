namespace NEquipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MeleeWepon : Wepon
    {
        public virtual void Action()
        {
        }

        public virtual void Start()
        {
            base.Start();
            name = "Wepon";
            actionKey = "Mouse0";
            coolTimeLength = 0.1f;
            isActive = true;
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
