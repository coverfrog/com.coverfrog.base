using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Gamo_0
{
    public class GameMgr : MonoBehaviour
    {
        [Title("Event")] 
        [SerializeField] private UnityEvent onStart;
        [SerializeField] private UnityEvent onLoadComplete;

        private void Start()
        {
            onStart?.Invoke();
        }
    }
}
