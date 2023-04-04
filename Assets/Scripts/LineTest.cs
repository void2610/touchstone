using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{
	LineRenderer line;
	public GameObject Hoge;
	public GameObject Puke;

	// Start is called before the first frame update
	void Start()
	{
		//コンポーネントを取得する
		this.line = GetComponent<LineRenderer>();

		//線の幅を決める
		this.line.startWidth = 0.1f;
		this.line.endWidth = 0.1f;

		//頂点の数を決める
		this.line.positionCount = 2;
	}

	// Update is called once per frame
	void Update()
	{
		//Updateに書いたのは作者が動的に変化させたかったため
		//0や1は頂点の順番(多分)
		line.SetPosition(0, Hoge.transform.position);
		line.SetPosition(1, Puke.transform.position);
	}
}