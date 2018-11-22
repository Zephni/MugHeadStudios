using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MugHeadStudios;

namespace MugHeadStudios_Example
{
	public class BaseScene_1 : MonoBehaviour
	{
		float sceneTime = 0;

		void Update()
		{
			sceneTime += Time.deltaTime;
			float x = 0;

			x = Global.Sin(2, 1, sceneTime);
			transform.position = new Vector2(x, 0);
			transform.Rotate(-Vector3.forward, Time.deltaTime * 50);

			if(Input.GetButtonDown("Fire1"))
			{
				BaseScene.instance.LoadScene("BaseScene_1");
			}
		}
	}
}