using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Cf.Scene
{
    public class SceneCtrl : MonoBehaviour
    {
        [Title("Text")]
        [SerializeField] private string codeName;
        [SerializeField] [TextArea] private string description;

        [Title("Event")] 
        [SerializeField] private UnityEvent onLoadComplete;
        [SerializeField] private UnityEvent onSceneStart;
    }
}
