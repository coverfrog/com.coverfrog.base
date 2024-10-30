using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;

namespace Cf.Net
{
    [RequireComponent(typeof(NetworkManager))]
    public abstract class NetMgr : MonoBehaviour
    {
        [Title("Gui")] 
        [SerializeField] private bool onGui;

        protected void Start()
        {
            var net = NetworkManager.Singleton;

            net.OnClientConnectedCallback += NetOnClientConnected.Callback;
        }

        protected void OnDestroy()
        {
            var net = NetworkManager.Singleton;

            if (!net)
            {
                return;
            }

            net.OnClientConnectedCallback -= NetOnClientConnected.Callback;
        }

        protected void OnGUI()
        {
            if (!onGui)
            {
                return;
            }

            var btnStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 30
            };

            var net = NetworkManager.Singleton;

            if (net.IsClient)
            {
                if (GUI.Button(new Rect(50, 50, 200, 75), "Exit", btnStyle))
                {
                    net.Shutdown();
                }
            }

            else
            {
                if (GUI.Button(new Rect(50, 50, 200, 75), "Host", btnStyle))
                {
                    net.StartHost();
                }

                if (GUI.Button(new Rect(50, 150, 200, 75), "Client", btnStyle))
                {
                    net.StartClient();
                }
            }
        }
    }
}
