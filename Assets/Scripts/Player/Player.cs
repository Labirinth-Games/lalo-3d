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
        public LevelManager levelManager;

        [Header("Tags Useds")]
        public string deadZoneTag = "End Map Zone";


        private SubscribeEventCalback _events = new SubscribeEventCalback();

        public void Spawn()
        {
            transform.position = SpawnManager.Instance.GetSpawn();
            transform.DOMoveY(2, .1f).SetEase(Ease.OutBounce);
        }

        #region --- Unity Events ---

        private void Start()
        {
            SpawnManager.Instance.OnHasAllSpawnPositions += Spawn;
        }

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

                if (Input.GetKeyDown(KeyCode.R))
                {
                    Application.LoadLevel(0);
                }
            }
            else Debug.Log("Ops sem controller para andar");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(deadZoneTag))
                Debug.Log("Ops Morreu");
        }

        private void OnDestroy()
        {
            SpawnManager.Instance.OnHasAllSpawnPositions -= Spawn;
        }
        #endregion
    }
}
