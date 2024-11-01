using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Cf.Scene
{
    [Serializable]
    public class SceneField
    {
        // base info this
        [SerializeField] private Object sceneAsset;
        [SerializeField] private string sceneName;
        
        // get name 
        public string SceneName => sceneName;
        
        // implicit : change Type , sceneField -> string
        public static implicit operator string(SceneField obj)
        {
            return obj.sceneName;
        }
    }
}
