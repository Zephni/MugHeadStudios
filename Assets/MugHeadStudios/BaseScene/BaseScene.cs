using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace MugHeadStudios
{
	public class BaseScene : MonoBehaviour
	{
		private static BaseScene _instance;
		public static BaseScene instance
		{
			get {return _instance;}
		}

		public string defaultScene;

		public string currentScene {get; private set;}

		void Awake()
		{
			_instance = this;
			if(defaultScene != null && defaultScene != "" && GameObject.Find("BaseLevelLoader") == null)
				LoadSceneAdditive(defaultScene);
		}

		public void LoadScene(string sceneName)
		{
			UnloadScene(currentScene);
			LoadSceneAdditive(sceneName);
		}

		public void LoadSceneAdditive(string sceneName)
		{
			currentScene = sceneName;
			SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
		}

		public void UnloadScene(string sceneName)
		{
			SceneManager.UnloadSceneAsync(sceneName);
		}
	}
}