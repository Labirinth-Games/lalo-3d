using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Labyrinth.Core;

namespace Lalo.Managers
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        public List<GameObject> prefabEnemiesList;

        [Header("Settings")]
        public int quantityEnemies = 3;

        private List<GameObject> _instancesEnemiesList;

        public void SpawnEnemiesOnWorld()
        {
            for(var i = 0; i < quantityEnemies; i++)
            {
                if (SpawnManager.Instance.HasPointSpawn())
                {
                    var enemy = prefabEnemiesList[0]; //[Random.Range(0, prefabEnemiesList.Count)];

                    var instance = Instantiate(enemy);
                    instance.transform.position = SpawnManager.Instance.GetSpawn();

                    _instancesEnemiesList.Add(instance);
                }
            }
        }

        #region --- Unity Events ---

        private void Start()
        {
            SpawnManager.Instance.OnHasAllSpawnPositions += SpawnEnemiesOnWorld;
            _instancesEnemiesList = new List<GameObject>();
        }

        private void OnDestroy()
        {
            SpawnManager.Instance.OnHasAllSpawnPositions -= SpawnEnemiesOnWorld;
        }

        #endregion
    }
}
