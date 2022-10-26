using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Labyrinth.Core;

namespace Lalo.Managers
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        public List<GameObject> spawnPositionsList;

        public string spawnPositionObjectName = "SpawnPosition";

        public System.Action OnHasAllSpawnPositions;

        public void GetAllSpawnPositions()
        {
            foreach (GameObject mazeSlice in LevelManager.Instance.mazeSliceList)
            {
                var spawnPosition = mazeSlice.transform.Find(spawnPositionObjectName);

                if (spawnPosition != null)
                    spawnPositionsList.Add(mazeSlice);
            }

            OnHasAllSpawnPositions?.Invoke();
        }

        public Vector3 GetSpawn()
        {
            int index = Random.Range(0, spawnPositionsList.Count);
            GameObject mazeSlice = spawnPositionsList[index];

            Vector3 mazePosition = mazeSlice.transform.position;
            Vector3 spawnPosition = mazeSlice.transform.Find(spawnPositionObjectName).transform.position;

            // remove at array to no remove more one in same place
            spawnPositionsList.RemoveAt(index);

            return spawnPosition + mazePosition;
        }

        public bool HasPointSpawn()
        {
            return spawnPositionsList.Count >= 0;
        }

        #region --- Unity Events ---
        private void Start()
        {
            LevelManager.Instance.OnGenerateMazeFinish += GetAllSpawnPositions;
        }

        private void OnDestroy()
        {
            LevelManager.Instance.OnGenerateMazeFinish -= GetAllSpawnPositions;
        }
        #endregion
    }
}
