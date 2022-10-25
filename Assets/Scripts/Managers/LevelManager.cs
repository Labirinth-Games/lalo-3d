using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Labyrinth.Core;

namespace Lalo.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [Header("References")]
        public List<GameObject> mazeSliceList;

        [Header("Settings")]
        public int level = 1;
        public int sizeMaze = 3; // 3x3 size maze

        // events callback
        public System.Action OnGenerateMazeFinish;

        private List<GameObject> _instances;

        public void GenerateMaze()
        {
            for(var i = 1; i <= sizeMaze; i++)
            {
                for(var j = 1; j <= sizeMaze; j++)
                {
                    var mazeSlice = mazeSliceList[Random.Range(0, mazeSliceList.Count)];
                    (float x, float z) size = GetSizeFloor(mazeSlice);
                    var instance = Instantiate(mazeSlice, gameObject.transform);

                    instance.transform.position = new Vector3(i * size.x, gameObject.transform.position.y, j * size.z);
                    
                    _instances.Add(instance);
                }

            }

            OnGenerateMazeFinish?.Invoke();
        }

        private (float, float) GetSizeFloor(GameObject floor)
        {
            var mesh = floor.transform.Find("Floor").GetComponent<MeshFilter>().sharedMesh;

            if (mesh == null)
                return (20f, 20f);

            return (mesh.bounds.size.x * 2, mesh.bounds.size.z * 2);
        }

        #region --- Unity Events ---
        private void Awake()
        {
            if (_instances == null)
                _instances = new List<GameObject>();
        }

        private void Start()
        {
            GenerateMaze();
        }
        #endregion
    }
}
