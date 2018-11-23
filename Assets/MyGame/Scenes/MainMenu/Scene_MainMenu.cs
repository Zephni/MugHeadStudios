using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_MainMenu : MonoBehaviour
{
	public void Button_NewGame()
	{
		SceneManager.LoadScene("SampleScene");
	}
}
