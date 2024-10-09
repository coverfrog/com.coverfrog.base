using System.Collections;
using System.Collections.Generic;
using CoverFrog;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Gamo_0
{
    public class CardMgr : MonoBehaviour
    {
        [Title("Data")] 
        [SerializeField] private Card cardPrefab;

        [Title("Spawn")] 
        [SerializeField] private Vector2Int spawnCardSize = new Vector2Int(50, 50);
        [SerializeField] private Vector2Int spawnSpaceSize = new Vector2Int(1, 1);
        
        [Title("Spawn - Debug")]
        [ShowInInspector] [ReadOnly] private Vector2Int _spawnCountMax = Vector2Int.zero;
        [ShowInInspector] [ReadOnly] private List<Card> _spawnCardList;
        
        private RectTransform _rtContent;
        private GridLayoutGroup _gridContent;
        
        // > 
        
        public void OnStart()
        {
            Cashing();
            SpawnMax();
            UpdateCardData();
        }
        
        // > 

        private void Cashing()
        {
            _rtContent ??= transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
            _gridContent ??= transform.GetChild(0).GetChild(1).GetComponent<GridLayoutGroup>();
            _spawnCardList ??= new List<Card>();
        }
        
        // > 

        private void UpdateSpawnCountMax()
        {
            var contentSize = _rtContent.rect.size;
            var count = contentSize / ( spawnCardSize + spawnSpaceSize );

            UpdateSpawnCount(count.x, count.y, out int x, out int y);

            _spawnCountMax = new Vector2Int(x, y);
        }
        
        // >
        
        private bool UpdateSpawnCount(float inputX, float inputY, out int x, out int y)
        {
            return UpdateSpawnCount(Mathf.FloorToInt(inputX), Mathf.FloorToInt(inputY), out x, out y);
        }

        private bool UpdateSpawnCount(int inputX, int inputY, out int x, out int y)
        {
            var inputCount = Mathf.FloorToInt(inputX * inputY);

            if (inputCount < 2)
            {
                x = 0;
                y = 0;
                
                return false;
            }

            if (inputCount % 2 == 0)
            {
                x = inputX;
                y = inputY;
                
                return true;
            }

            if (inputX > 1)
            {
                inputX -= 1;
            }

            else if (inputY > 1)
            {
                inputY -= 1;
            }

            x = inputX;
            y = inputY;

            return true;
        }

        // >

        private void UpdateCardData()
        {
            if (_spawnCardList == null || _spawnCardList.Count <= 0)
            {
                return;
            }

            var count = _spawnCardList.Count;
            var halfCount = count / 2;
            var idxList = new List<int>(count);
            var ranIdxList = UtilMath.GetNoneOverlapNumbers(count, count);

            for (var _ = 0; _ < 2; _++)
            {
                for (var idx = 0; idx < halfCount; idx++)
                {
                    idxList.Add(idx);
                }
            }

            for (var idx = 0; idx < _spawnCardList.Count; idx++)
            {
                var card = _spawnCardList[idx];
                var targetIdx = idxList[ranIdxList[idx]];
                
                card.OnData(targetIdx);
            }
        }

        // >
        
        private void SpawnMax()
        {
            UpdateSpawnCountMax();
            Spawn(_spawnCountMax.x * _spawnCountMax.y);
        }

        private void Spawn(int spawnCount)
        {
            _gridContent.cellSize = spawnCardSize;
            _gridContent.spacing = spawnSpaceSize;
            _gridContent.childAlignment = TextAnchor.MiddleCenter;

            for (var _ = 0; _ < spawnCount; _++)
            {
                var card = Instantiate(original: cardPrefab, parent: _rtContent);
                _spawnCardList.Add(card);
            }

            for (var idx = 0; idx < spawnCount; idx++)
            {
                _spawnCardList[idx].OnSpawn(this);
            }
        }
    }
}
