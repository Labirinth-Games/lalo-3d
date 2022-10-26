using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Labyrinth.Core;
using DG.Tweening;

namespace Lalo.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [Header("References")]
        public List<GameObject> mazeSliceList;
        public GameObject deadZone;

        [Header("Settings")]
        public int level = 1;
        public int sizeMaze = 3; // 3x3 size maze

        // events callback
        public Action OnGenerateMazeFinish;

        private List<GameObject> _instances;
        private GameObject _deadZoneInstance;

        public void GenerateMaze()
        {
            gameObject.transform.position = Vector3.zero;

            for (var i = 0; i < sizeMaze; i++)
            {
                for(var j = 0; j < sizeMaze; j++)
                {
                    var mazeSlice = mazeSliceList[UnityEngine.Random.Range(0, mazeSliceList.Count)];
                    var instance = Instantiate(mazeSlice, gameObject.transform);
                    (float x, float z) size = GetSizeFloor(mazeSlice);

                    instance.transform.position = new Vector3(i * size.x, gameObject.transform.position.y, j * size.z);

                    _instances.Add(instance);
                }

            }

            AddedDeadZone();

            OnGenerateMazeFinish?.Invoke();
        }

        private (float, float) GetSizeFloor(GameObject floor)
        {
            var mesh = floor.transform.Find("Floor").GetComponent<MeshFilter>().sharedMesh;

            if (mesh == null)
                return (20f, 20f);

            return (mesh.bounds.size.x * 2, mesh.bounds.size.z * 2);
        }

        private void AddedDeadZone()
        {
            _deadZoneInstance = Instantiate(deadZone, gameObject.transform);
            float size = sizeMaze * sizeMaze;

            _deadZoneInstance.transform.localScale = new Vector3(size, 1, size);
            _deadZoneInstance.transform.DOMoveY(-10, 0);
        }

        #region --- Unity Events ---
 
        private void Start()
        {
            if (_instances == null)
                _instances = new List<GameObject>();

            GenerateMaze();
        }
        #endregion
    }
}
