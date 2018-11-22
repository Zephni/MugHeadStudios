using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MugHeadStudios;

namespace MugHeadStudios_Example
{
	public class MessageBoxExample : MonoBehaviour
	{
		[TextArea]
		public string messageExample = "This is a MessageBox example";

		public Text debug;

		void Start()
		{
			Global.WaitRun(1, () => {
				MessageBoxController.instance.Build(new MessageBoxParams{
					Position = Vector2.zero,
					Text = "MessageBox Example\nThis is an example for the MessageBox system!"
				});
			});
		}

		void Update()
		{
			debug.text = "RunningActions: "+ Global.RunningActions.Count.ToString();
		}
	}
}