using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MuPuzzle.Content
{
    public abstract class ContentHandler<TData, TAct> : MonoBehaviour where TData : ContentData where TAct : ContentAct
    {
        [TitleGroup("Base")] 
        [ShowInInspector] [ReadOnly] protected ContentData Data { get; private set; }
        [ShowInInspector] [ReadOnly] protected ContentAct Act { get; private set; }
        
        private void Awake()
        {
            Init();
            ContentStart();
        }

        private void Init()
        {
            Data = ScriptableObject.CreateInstance<TData>();
            Data.name = "Data";
            
            Act = ScriptableObject.CreateInstance<TAct>();
            Act.name = "Act";
        }

        private void ContentStart()
        {
            
        }
    }
}
