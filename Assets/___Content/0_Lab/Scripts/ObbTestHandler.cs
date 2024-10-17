using System;
using System.Collections;
using System.Collections.Generic;
using Cf.Util;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cf.Lab
{
    public class ObbTestHandler : MonoBehaviour
    {
        [Title("List")]
        public List<RectTransform> rtList;

        [Title("Point")] 
        public RectTransform startPoint;
        public RectTransform endPoint;

        [Title("Obb")] 
        public ObbTest obbTestPrefab;

        public void Run()
        {
            if (_coRun != null)
            {
                return;
            }

            _coRun = CoRun();
            StartCoroutine(_coRun);
        }

        private void FindEmpty(float width)
        {
            
        }

        private IEnumerator _coRun;

        private IEnumerator CoRun()
        {
            var size = obbTestPrefab.rt.sizeDelta;
            FindEmpty(size.x);
            
            var ins = Instantiate(original: obbTestPrefab, parent: startPoint);
            ins.rt.anchoredPosition = Vector2.zero;
            ins.enabled = true;

            var s =  RectTransformUtility.WorldToScreenPoint(null, ins.rt.position);
            var e = RectTransformUtility.WorldToScreenPoint(null, endPoint.position);
            var m = (e - s);
            
            var duration = 3.0f;
            for (var t = 0.0f; t <= duration; t += Time.deltaTime)
            {
                var percent = t / duration;
                
                var addY = Mathf.Sin(Mathf.PI * percent) * 500.0f;
                
                ins.rt.position = Vector2.Lerp(s, e, percent) + new Vector2(0, addY);
                
                yield return null;
            }

            _coRun = null;
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(50, 50, 150, 50), "Throw"))
            {
                Run();
            }
        }
    }
}
