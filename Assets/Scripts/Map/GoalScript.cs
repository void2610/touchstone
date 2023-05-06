namespace NMap
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using NManager;

	public class GoalScript : MonoBehaviour
	{
		private GameManager gm;
		void Start()
		{
			gm = GameObject.Find("GameController").GetComponent<GameManager>();
		}

		// Update is called once per frame
		void Update()
		{
			//少しづつ回転させる
			this.transform.Rotate(new Vector3(0, 0, 0.03f));
			//少し上下に動かす
			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + Mathf.Sin(Time.time) * 0.001f, this.transform.position.z);
		}

		//衝突判定
		void OnTriggerEnter2D(Collider2D other)
		{
			//プレイヤーと衝突したら
			if (other.gameObject.name == "Player")
			{
				//ゲームクリア
				gm.state = GameManager.GameState.Clear;
			}
		}
	}
}