using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MugHeadStudios
{
    public class RunningAction
    {
        public Func<bool> until;
        public Action action;
        public Action callback;

        public RunningAction(Func<bool> _until, Action _action, Action _callback = null)
        {
            until = _until;
            action = _action;
            callback = _callback;
        }
    }
}