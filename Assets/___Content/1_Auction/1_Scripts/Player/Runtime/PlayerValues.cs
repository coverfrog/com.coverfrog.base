using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Auc.Player
{
    [Serializable]
    public class PlayerValues 
    {
        [ShowInInspector] public bool IsMovePressed { get; set; }
    }
}
