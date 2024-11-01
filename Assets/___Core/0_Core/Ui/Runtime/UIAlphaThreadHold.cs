using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Cf.Ui
{
    public class UIAlphaThreadHold : MonoBehaviour
    {
        [Title("Global")]
        [SerializeField] private bool isModifyWhenAwake = true;
        [SerializeField] private bool targetImageIsMine = true;
        [SerializeField] [HideIf("targetImageIsMine")] private bool targetIsMulti = false;
        
        [Title("Source")]
        [SerializeField] [HideIf("@targetImageIsMine || targetIsMulti")] private Image imgSingle;
        
        [Title("Source")]
        [SerializeField] [HideIf("@targetImageIsMine || !targetIsMulti")] private List<Image> imgList;
        
        [Title("Value")]
        [SerializeField] [Range(0.000001f, 1.0f)] private float threshold = 0.001f;

        private void Awake()
        {
            if (!isModifyWhenAwake)
            {
                return;
            }

            ModifyThresholdBegin();
        }

        public void ModifyThresholdBegin()
        {
            if (targetImageIsMine)
            {
                ModifyThreshold(imgSingle);
            }

            else
            {
                foreach (Image img in imgList)
                {
                    if (!img)
                    {
                        continue;
                    }
                    
                    ModifyThreshold(img);
                }
            }
        }

        private void ModifyThreshold(Image img)
        {
            if (targetImageIsMine)
            {
                img = GetComponent<Image>();
            }

            if (!img)
            {
                Debug.LogError("Img Is Not Found");
                return;
            }

            Sprite sprite = img.sprite;

            if (!sprite)
            {
                Debug.LogError("Sprite Is Not Found");
                return;
            }

            Texture2D texture = sprite.texture;
            bool isReadable = texture.isReadable;

            if (!isReadable)
            {
                Debug.LogError("IsReadable Is Not Active");
                return;
            }

            img.alphaHitTestMinimumThreshold = threshold;
        }
    }
}
