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
        /// クールタイムの開始時間
        /// </summary>
        public float coolStartTime;

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
        /// 装備の効果の開始時間
        /// </summary>
        public float activeStartTime;

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
            activeStartTime = Time.time;
        }

        public virtual void Start()
        {
            name = "NoName";
            actionKey = "NoKey";
            coolTimeLength = 0.0f;
            coolStartTime = Mathf.Infinity;
            isCooling = false;
            isEnable = true;
            activeTimeLength = 0.0f;
            activeStartTime = Mathf.Infinity;
            isActive = false;

            icon = Resources.Load<Sprite>("Sprites/Equipment/" + name);
        }

        public virtual void Update()
        {
            if (Time.time - activeStartTime > activeTimeLength)
            {
                isActive = false;
            }
            if (Time.time - coolStartTime > coolTimeLength)
            {
                isCooling = false;
            }
        }

        public virtual void FixedUpdate()
        {
            if (Input.GetKeyDown(actionKey) && isEnable)
            {
                if (!isCooling)
                {
                    Action();
                    isCooling = true;
                }
            }
        }
    }
}
