using System;
using System.Collections;
using System.Collections.Generic;
using Cf.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Cf.Lab
{
    public class ObbTest : MonoBehaviour
    {
        public Image img;
        public RectTransform rt;
        
        public bool isDown;
        public bool isRotate;
        
        private ObbTestHandler Handler;

        private void Awake()
        {
            Handler = FindObjectOfType<ObbTestHandler>();
        }

        private void OnEnable()
        {
            Handler.rtList.Add(rt);
        }

        private void Update()
        {
            // transform.localEulerAngles += Vector3.forward * (20.0f * Time.deltaTime);

            isDown = !UtilRect.ObbCollision(Handler.rtList, rt, out var colCount);
            
            img.color = isDown ? Color.red : Color.green;

            if (!isDown)
            {
                return;
            }
            
            rt.anchoredPosition -= Vector2.up * (100.0F * Time.deltaTime);
        }
    }
}
