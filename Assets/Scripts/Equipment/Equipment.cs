namespace NEquipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Equipment : MonoBehaviour
    {
        /// <summary>
        /// 装備の名前
        /// </summary>
        public string name;

        /// <summary>
        /// 装備を使用するためのキー
        /// </summary>
        public string actionKey;

        /// <summary>
        /// クールタイムの長さ
        /// </summary>
        public float coolTimeLength;

        /// <summary>
        /// クールタイム中かどうか
        /// </summary>
        public bool isCooling;

        /// <summary>
        /// 装備を使用可能かどうか
        /// </summary>
        public bool isEnable;

        /// <summary>
        /// 装備が効果を発揮する時間の長さ
        /// </summary>
        public float activeTimeLength;

        /// <summary>
        /// 装備が効果を発揮しているかどうか
        /// </summary>
        public bool isActive;

        /// <summary>
        /// 装備のアイコン画像
        /// </summary>
        public Sprite icon;

        public void ResetCoolTime()
        {
            isCooling = false;
        }

        public virtual void Action()
        {
            isActive = true;
            Invoke("ResetActionTime", activeTimeLength);
        }

        public virtual void Start()
        {
            name = "NoName";
            actionKey = "NoKey";
            coolTimeLength = 0.0f;
            isCooling = false;
            isEnable = true;
            activeTimeLength = 0.0f;
            isActive = false;

            icon = Resources.Load<Sprite>("Sprites/Equipment/" + name);
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
            if (Input.GetKeyDown(actionKey) && isEnable)
            {
                if (!isCooling)
                {
                    Action();
                    isCooling = true;
                    Invoke("ResetCoolTime", coolTimeLength);
                }
            }
        }
    }
}
