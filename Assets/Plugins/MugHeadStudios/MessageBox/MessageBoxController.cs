using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MugHeadStudios
{
    public class MessageBoxController : MonoBehaviour
    {
        public static MessageBoxController instance {
            get; private set;
        }    

        public void Awake()
        {
            instance = this;
        }

        public List<GameObject> messageBoxPrefabs;

        public void Build(MessageBoxParams parameters, Action<MessageBox> callback = null)
        {
            GameObject go = Instantiate(messageBoxPrefabs[parameters.PrefabIndex], Vector3.zero, Quaternion.identity);
            go.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 1);
            go.transform.SetParent(this.transform);
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<MessageBox>().text = parameters.Text;
            go.GetComponent<MessageBox>().writeTextSpeed = parameters.Speed;

            Vector2 pos = Camera.main.WorldToViewportPoint((parameters.Position != null) ? (Vector3)parameters.Position : Camera.main.transform.position);
            go.GetComponent<RectTransform>().anchorMin = pos;
            go.GetComponent<RectTransform>().anchorMax = pos;

            Global.RunFor(parameters.FadeTime, i => {
                go.GetComponent<RectTransform>().localScale = new Vector3(i, i, 1);
                go.GetComponent<CanvasGroup>().alpha = i;
            }, () => {
                go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                go.GetComponent<CanvasGroup>().alpha = 1;
            });

            go.GetComponent<MessageBox>().WriteText(() => {
                if (callback != null) callback(go.GetComponent<MessageBox>());
            });
        }

        List<MessageBoxParams> messageBoxList;
        Await messageBoxsAwait;
        public Await BuildMessages(params MessageBoxParams[] parameters)
        {
            messageBoxList = new List<MessageBoxParams>(parameters);

            Build(messageBoxList[0], mb => {
                Global.RunUntil(() => { return Input.GetButtonDown("Fire1"); }, () => { }, () => {
                    Global.RunFor(parameters[0].FadeTime, i => {
                        mb.GetComponent<RectTransform>().localScale = new Vector3(1 - i, 1 - i, 1);
                        mb.GetComponent<CanvasGroup>().alpha = 1-i;
                    }, () => {
                        DestroyImmediate(mb.gameObject);
                        messageBoxList.RemoveAt(0);
                        if(messageBoxList.Count > 0)
                            BuildMessages(messageBoxList[0]);
                        else
                        {
                            Action temp = messageBoxsAwait.callback;
                            messageBoxsAwait = null;
                            if (temp != null)  temp();
                        }
                    });
                });
            });

            if(messageBoxsAwait == null)
                messageBoxsAwait = new Await();
            return messageBoxsAwait;
        }

        public Await BuildMessages(params string[] parameters)
        {
            List<MessageBoxParams> mbp = new List<MessageBoxParams>();
            foreach(var item in parameters)
                mbp.Add(new MessageBoxParams{Text = item});
            
            return BuildMessages(mbp.ToArray());
        }
    }

    public class MessageBoxParams
    {
        public string Text = "No text";
        public float Speed = 50;
        public float FadeTime = 0.15f;
        public Vector3? Position = null;
        public int PrefabIndex = 0;
    }
}