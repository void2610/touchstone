namespace NManager
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using DG.Tweening;

	public class CameraMoveScript : MonoBehaviour
	{
		[SerializeField]
		float offset;
		public GameObject player;
		private Vector3 basePosition;
		private Vector3 shakeOffset;

		private void Start()
		{
			// 初期位置を設定
			Vector2 pos = player.transform.position;
			basePosition = new Vector3(0, pos.y + offset, -10);
			shakeOffset = Vector3.zero;
			this.transform.position = basePosition;
		}

		public void ShakeCamera(float duration = 0.3f, float strength = 1.5f)
		{
			this.transform.DOComplete();
			DOTween.Shake(() => shakeOffset, x => shakeOffset = x, duration, strength).OnComplete(() => shakeOffset = Vector3.zero);
		}

		private void LateUpdate()
		{
			// プレイヤーの位置に基づいてベース位置を更新
			Vector2 pos = player.transform.position;
			basePosition = new Vector3(0, pos.y + offset, -10);
			// カメラの実際の位置をベース位置と揺れのオフセットの合計に設定
			this.transform.position = basePosition + shakeOffset;
		}
	}
}
