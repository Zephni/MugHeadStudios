using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MugHeadStudios
{
    [ExecuteInEditMode]
    public static class Global
    {
        private static List<string> DoOnceTracker;

        public static List<RunningAction> RunningActions
        {
            get; private set;
        }

        public static void Awake()
        {
            DoOnceTracker = new List<string>();
            RunningActions = new List<RunningAction>();
        }

        public static void Update()
        {
            if(RunningActions != null)
            for (int i = 0; i < RunningActions.Count; i++)
            {
                RunningAction ra = RunningActions[i];

                if (ra.until())
                {
                    RunningActions.Remove(ra);
                    if (ra.callback != null)
                        ra.callback();
                }
                else
                {
                    ra.action();
                }
            }
        }

        public static List<T> CreateList<T>(params T[] items)
        {
            List<T> list = new List<T>(items);
            return list;
        }

        public static void For(int startInclusive, int endInclusive, int step, Action<int> action)
        {
            for (int i = startInclusive; (step > 0) ? i <= endInclusive : i <= endInclusive; i += step)
                action(i);
        }

        public static void For(int startInclusive, int endInclusive, Action<int> action)
        {
            int step = (endInclusive > startInclusive) ? 1 : -1;
            For(startInclusive, endInclusive, step, action);
        }

        public static void For<T>(List<T> list, Action<int> action)
        {
            For(0, list.Count-1, 1, action);
        }

        public static void RunOnce(string key, bool boolean, Action action)
        {
            if (!DoOnceTracker.Contains(key) && boolean)
            {
                DoOnceTracker.Add(key);
                action();
            }
        }

        public static void RunWhenEventLoops(string key, bool boolean, Action action)
        {
            if (!DoOnceTracker.Contains(key) && boolean)
            {
                DoOnceTracker.Add(key);
                action();
            }
            else if (DoOnceTracker.Contains(key) && !boolean)
            {
                DoOnceTracker.Remove(key);
            }
        }

        public static T Use<T>(Action<T> action = null)
        {
            T obj = default(T);
            if(action != null) action(obj);
            return obj;
        }

        public static T Use<T>(Func<T> createObject, Action<T> action = null)
        {
            T obj = createObject();
            if (action != null) action(obj);
            return obj;
        }

        public static void RunUntil(Func<bool> until, Action action, Action callback = null)
        {
            RunningActions.Add(new RunningAction(
                until, 
                action, 
                callback));
        }

        public static void WaitRun(float time, Action callback)
        {
            float t = 0;
            RunUntil(() => {t += Time.deltaTime; return t >= time;}, () => {}, callback);
        }

        public static void RunFor(float totalTime, Action<float> action, Action callback = null)
        {
            Global.Use<float>(i =>
            {
                Global.RunUntil(() => {return i >= totalTime; }, () => {
                    i += Time.deltaTime;
                    action(i / totalTime);
                }, () =>{
                    if(callback != null)
                        callback();
                });
            });
        }

        public static float Sin(float distance, float speed = 1, float? progress = null)
        {
            float p = (progress == null) ? Time.timeSinceLevelLoad : (float)progress;
            return distance * Mathf.Sin(p * speed);
        }

        public static T ModifyNonVariable<T>(T obj, Func<T, T> act)
        {
            T newObj = obj;
            newObj = act(newObj);
            return newObj;
        }
    }

    public class Await
    {
        public Action callback;

        public void Next(Action _callback)
        {
            callback = _callback;
        }
    }
}