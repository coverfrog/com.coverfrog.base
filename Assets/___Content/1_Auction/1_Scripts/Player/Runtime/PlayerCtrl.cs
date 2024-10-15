using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Auc.InputAct;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Auc.Player
{
    public class PlayerCtrl : Cf.Player.PlayerCtrl
    {
        [Title("Values")] 
        [ShowInInspector] [ReadOnly] private PlayerValues _playerValues;
        
        private AucInputModule _module;

        #region > Unity 

        private void Awake()
        {
            _playerValues = new PlayerValues();
            _module = new AucInputModule();
            _module.Player.Enable();
        }

        private void OnEnable()
        {
            _module.Player.Move.performed += MoveOnPerformed;
        }

        private void OnDisable()
        {
            _module.Player.Move.performed -= MoveOnPerformed;
        }

        #endregion

        #region > Event - Input

        private void MoveOnPerformed(InputAction.CallbackContext context)
        {
            _playerValues.IsMovePressed = context.ReadValue<float>() > 0;
        }
        
        #endregion
    }
}
