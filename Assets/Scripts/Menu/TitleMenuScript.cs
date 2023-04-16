using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuScript : MonoBehaviour
{
	[SerializeField]
	private GameObject titleText;
	[SerializeField]
	private GameObject startButton;
	[SerializeField]
	private GameObject settingButton;
	[SerializeField]
	private GameObject backButton;
	[SerializeField]
	private GameObject quitButton;
	[SerializeField]
	private GameObject entryButton;

	private int state = 0;
	//0:タイトル画面 1:装備編成画面 2:設定画面

	public void OnClickStartButton()
	{
		state = 1;
	}

	public void OnClickSettingButton()
	{
		state = 2;
	}

	public void OnClickBackButton()
	{
		state = 0;
	}

	public void OnClickQuitButton()
	{
		//ゲーム終了
		Application.Quit();
		//エディターでプレイモードを終了
		UnityEditor.EditorApplication.isPlaying = false;
	}

	public void OnClickEntryButton()
	{
		//ゲームシーンへ
		SceneManager.LoadScene("SampleScene");
	}

	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (state == 0)
		{
			titleText.GetComponent<Text>().text = "Tower of the Dead";
			startButton.SetActive(true);
			settingButton.SetActive(true);
			backButton.SetActive(false);
			quitButton.SetActive(true);
			entryButton.SetActive(false);

		}
		else if (state == 1)
		{
			titleText.GetComponent<Text>().text = "Equipment organization";
			startButton.SetActive(false);
			settingButton.SetActive(false);
			backButton.SetActive(true);
			quitButton.SetActive(false);
			entryButton.SetActive(true);
		}
		else if (state == 2)
		{
			titleText.GetComponent<Text>().text = "Setting";
			startButton.SetActive(false);
			settingButton.SetActive(false);
			backButton.SetActive(true);
			quitButton.SetActive(false);
			entryButton.SetActive(false);
		}
	}
}
