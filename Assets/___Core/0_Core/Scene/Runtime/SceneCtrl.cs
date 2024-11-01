using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Cf.Scene
{
    public class SceneCtrl : MonoBehaviour
    {
        [Title("Text")]
        [SerializeField] private string codeName;
        [SerializeField] [TextArea] private string description;

        [Title("Load With")] 
        [SerializeField] private List<SceneField> loadWithSceneList;
        
        // with load scene all
        public IEnumerator AsyncLoadWithSceneList(Action<float> onUnitProgress, Action<float> onTotalProgress)
        {
            // check list count
            if (loadWithSceneList.Count <= 0)
            {
                yield break;
            }

            // to read only
            IReadOnlyList<SceneField> loadSceneList = loadWithSceneList;
            
            // value
            int loadIndex = 0;
            float segment = 1.0f / loadSceneList.Count;
            
            foreach (SceneField sceneField in loadSceneList)
            {
                // get operation
                AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneField, LoadSceneMode.Additive);
                if (loadOperation == null)
                {
                    yield break;
                }
                
                // null check
                bool isUnitActionExist = onUnitProgress != null;
                bool isTotalActionExist = onTotalProgress != null;
                
                // run operation
                while (!loadOperation.isDone)
                {
                    if (isUnitActionExist)
                    {
                        float unitPercent = loadOperation.progress / 0.9f;
                        onUnitProgress.Invoke(unitPercent);
                    }

                    if (isTotalActionExist)
                    {
                        float totalPercent = segment * loadIndex + segment * loadOperation.progress / 0.9f;
                        onTotalProgress.Invoke(totalPercent);
                    }

                    yield return null;
                }
                
                // idx
                ++loadIndex;
            }
            
            // active
            foreach (string sceneName in loadSceneList)
            {
                UnityEngine.SceneManagement.Scene scene = SceneManager.GetSceneByName(sceneName);
                SceneManager.SetActiveScene(scene);
            }
        }
    }
}
