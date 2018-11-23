using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

namespace MugHeadStudios
{
	[ExecuteInEditMode]
	public class MessageBox : MonoBehaviour
	{
		private GameObject textObject;

		public string fireButton = "Fire1";

		public float speedUpMultiplier = 2;

		public bool canSpeedUpWrite = true;

		public enum BoxType{TextRelative, TextRelativeOnWrite, Custom}

		public BoxType boxType = BoxType.TextRelative;

		public Vector2 padding = new Vector2(20, 20);

		public bool writeText = true;

		public float writeTextSpeed = 100;

		public string text;
		
		public void Update()
		{
			textObject = transform.Find("Text").gameObject;
			textObject.GetComponent<Text>().text = text;
			Vector2 size = textObject.GetComponent<RectTransform>().sizeDelta;
			GetComponent<RectTransform>().sizeDelta = size + padding;
		}

		public void WriteText(Action callback = null)
		{
			textObject = transform.Find("Text").gameObject;
			textObject.GetComponent<Text>().text = text;
			Vector2 size = textObject.GetComponent<RectTransform>().sizeDelta;
			GetComponent<RectTransform>().sizeDelta = size + padding;

			if(!writeText)
			{
                if (callback != null) callback();
			}
			else
			{
				GameObject writingTextObject = GameObject.Instantiate(textObject);
				writingTextObject.transform.SetParent(textObject.transform);
				ContentSizeFitter csfDestroy = writingTextObject.GetComponent<ContentSizeFitter>();
				DestroyImmediate(csfDestroy);
				writingTextObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
				writingTextObject.GetComponent<RectTransform>().sizeDelta = textObject.GetComponent<RectTransform>().sizeDelta;
				Text writingTextComponent = writingTextObject.GetComponent<Text>();
				writingTextComponent.text = "";

				Text textComponent = textObject.GetComponent<Text>();
				textComponent.color = Color.white * 0;

				writingTextObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, textComponent.transform.localPosition.z);
				writingTextObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
				writingTextObject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
				writingTextObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

				float i = 0;
				float textProgress = 0;
				Global.RunUntil(() => { return writingTextComponent.text == textComponent.text; }, () => {
					i += (canSpeedUpWrite && Input.GetButton(fireButton)) ? Time.deltaTime * writeTextSpeed * speedUpMultiplier : Time.deltaTime * writeTextSpeed;
					
					if(i >= 1)
					{
						textProgress += i;
						if(textProgress > textComponent.text.Length) textProgress = textComponent.text.Length;
						writingTextComponent.text = textComponent.text.Substring(0, Mathf.FloorToInt(textProgress));
						i = i - 1;
					}			
				}, () => {
					textComponent.color = Color.white * 1f;
					DestroyImmediate(writingTextObject);
					if(callback != null) callback();
				});
			}
		}
	}

	[CustomEditor(typeof(MessageBox))]
	public class MessageBoxInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			MessageBox myScript = (MessageBox)target;

			myScript.text = EditorGUILayout.TextField(myScript.text, GUILayout.Height(60));

			myScript.boxType = (MessageBox.BoxType)EditorGUILayout.EnumPopup("Box Type", myScript.boxType);

			myScript.padding = EditorGUILayout.Vector2Field("Padding", myScript.padding);

			myScript.writeText = EditorGUILayout.Toggle("Write out text", myScript.writeText);

			if(myScript.writeText)
				myScript.writeTextSpeed = EditorGUILayout.FloatField("Write Text Speed", myScript.writeTextSpeed);

			if (GUILayout.Button("This does nothing"))
			{
				
			}
		}
	}
}