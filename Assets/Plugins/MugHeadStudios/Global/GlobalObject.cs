using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MugHeadStudios
{
    [ExecuteInEditMode]
    public class GlobalObject : MonoBehaviour
    {
        void Awake()
        {
            Global.Awake();
        }

        void Update()
        {
            Global.Update();
        }
    }
}