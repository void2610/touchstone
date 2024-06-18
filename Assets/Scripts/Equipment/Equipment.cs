namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;
	using UnityEngine.UI;

	public class Equipment : MonoBehaviour
	{
		/// <summary>
		/// 装備の名前
		/// </summary>
		public string equipmentName;

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
		/// クールタイムの開始時間
		/// </summary>
		public float coolStartTime;

		/// <summary>
		/// 装備を使用可能かどうか
		/// </summary>
		public bool isEnable;

		/// <summary>
		/// 装備が効果を発揮する時間の長さ
		/// </summary>
		public float activeTimeLength;

		/// <summary>
		/// 装備が効果を発揮する時間の開始時間
		/// </summary>
		public float activeStartTime = 0;

		/// <summary>
		/// 装備が効果を発揮する時間の開始時の角度
		/// </summary>
		public float activeStartAngle = 0;

		/// <summary>
		/// 装備が効果を発揮する時間の開始時のマウス位置
		/// </summary>
		public Vector2 activeStartPosition = Vector2.zero;

		/// <summary>
		/// 装備が効果を発揮しているかどうか
		/// </summary>
		public bool isActive;

		/// <summary>
		/// マウスの角度
		/// </summary>
		public float angle;

		public GameObject player;

		public Image gauge;

		public void Init(GameObject player, Image gauge)
		{
			this.player = player;
			this.gauge = gauge;
		}

		protected void CutHP(Character target, int atk)
		{
			target.hp -= atk;
			Debug.Log(target.characterName + "のHPが" + atk + "削れた");
		}

		protected float getMouseAngle()
		{
			Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
			float res = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			return res;
		}

		protected Vector2 getMousePosition()
		{
			Vector2 res = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			return res;
		}

		protected virtual IEnumerator CoolTime()
		{
			isCooling = true;
			coolStartTime = Time.time;
			yield return new WaitForSeconds(coolTimeLength);
			isCooling = false;
			yield break;
		}

		//長押しでactiveTimeLengthの時間まで有効、長押しを離したらクールタイムが始まる
		public virtual IEnumerator Action()
		{
			activeStartTime = Time.time;
			isActive = true;
			activeStartAngle = angle;
			activeStartPosition = getMousePosition();
			OnActionStart();
			yield return new WaitForSeconds(activeTimeLength);
			if (isActive)
			{
				isActive = false;
				OnActionEnd();
				StartCoroutine(CoolTime());
			}
		}

		protected virtual void OnActionStart()
		{
		}

		protected virtual void Effect()
		{
		}

		protected virtual void OnActionEnd()
		{
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
			player = GameObject.Find("Player");

		}

		protected virtual void Update()
		{
			angle = getMouseAngle();
			if (Input.GetButton(actionKey) && isEnable && !isCooling && !isActive)
			{
				StartCoroutine(Action());
			}

			if (isActive && Input.GetButtonUp(actionKey))
			{
				//長押しを離したらクールタイムが始まる
				isActive = false;
				activeStartTime = 0;
				OnActionEnd();
				StartCoroutine(CoolTime());
			}

			if (isCooling)
			{
				gauge.fillAmount = 1 - ((Time.time - coolStartTime) / coolTimeLength);
			}
		}

		protected virtual void FixedUpdate()
		{
			if (isActive)
			{
				Effect();
			}
		}
	}
}
