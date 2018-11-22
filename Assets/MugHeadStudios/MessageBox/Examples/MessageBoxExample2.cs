using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MugHeadStudios;

namespace MugHeadStudios_Example
{
	public class MessageBoxExample2 : MonoBehaviour
	{
		MessageBoxController MessageBoxController
		{
			get {return MessageBoxController.instance;}
		}

		void Start()
		{
			Global.WaitRun(1, () => {
				MessageBoxController.BuildMessages(
					"MessageBox Example\n" + "Press Fire1 to continue to the next message.",
					"You can use .Next() method to run something AFTER a\nset of messages."
				).Next(() => {
					Global.WaitRun(0.5f, () => {
						MessageBoxController.BuildMessages(
							new MessageBoxParams { Text = "MessageBoxExample\n" + "Each message can have it's own set of parameters! :)", Position = new Vector2(0, 2) },
							new MessageBoxParams { Text = "This one is much slower for example...", FadeTime = 1.6f, Speed = 25 }
						);
					});
				});
			});
		}
	}
}