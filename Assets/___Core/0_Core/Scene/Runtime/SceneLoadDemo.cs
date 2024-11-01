using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cf.Scene
{
    public class SceneLoadDemo : MonoBehaviour
    {
        [SerializeField] private SceneCtrl sceneCtrl;

        private void Start()
        {
            IEnumerator loadAsync = sceneCtrl.AsyncLoadWithSceneList(OnUnitProgress, OnTotalProgress);
            StartCoroutine(loadAsync);
        }

        private void OnUnitProgress(float percent)
        {
            
        }

        private void OnTotalProgress(float percent)
        {
            
        }
    }
}
