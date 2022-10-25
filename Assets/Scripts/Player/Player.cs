using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Labyrinth.Core;
using Labyrinth.Helpers;
using Lalo.Managers;
using DG.Tweening;

namespace Lalo
{
    public class Player : Singleton<Player>
    {
        [Header("References")]
        public SOPlayerSettings playerSettings;
        public ControllerHelper controllerHelper;


        //private SubscribeEventCalback<Player> events;

        public void Spawn()
        {
            transform.position = LevelManager.Instance.transform.position;
            transform.DOMoveY(10, 1f);

            Debug.Log("chamou aqui");
        }

        #region --- Unity Events ---

        private void OnValidate()
        {
            if (controllerHelper == null)
                controllerHelper = GetComponent<ControllerHelper>();
        }

        void Update()
        {
            if (controllerHelper != null)
            {
                controllerHelper.Move();
                controllerHelper.Jump();
            }
            else Debug.Log("Ops sem controller para andar");
        }
        #endregion
    }
}
