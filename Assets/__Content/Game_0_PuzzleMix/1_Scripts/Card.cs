using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gamo_0
{
    public class Card : MonoBehaviour
    {
        private CardMgr _cardMgr;
        private RectTransform _rtMine;
        private Text _txt;
        
        // >
        
        public void OnSpawn(CardMgr cardMgr)
        {
            _cardMgr = cardMgr;
            
            Cashing();

            _rtMine.anchorMin = new Vector2(0.5f, 0.5f);
            _rtMine.anchorMax = new Vector2(0.5f, 0.5f);
            _rtMine.pivot = new Vector2(0.5f, 0.5f);
        }

        public void OnData(int idx)
        {
            _txt.text = idx.ToString();
        }

        // >

        private void Cashing()
        {
            _rtMine = GetComponent<RectTransform>();
            _txt = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        }
    }
}
