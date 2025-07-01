using System.Collections.Generic;
using UnityEngine;

namespace Remnants
{
    public class ObjectsSpawnManager : MonoBehaviour
    {
        #region Variables
        public List<Transform> spawnPoints;
        public List<GameObject> spheres;

        [SerializeField]
        private float spawnInterval = 3f;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            InvokeRepeating(nameof(SpawnRandomSphere), 0f, spawnInterval);
        }
        #endregion

        #region Custom Method
        private void SpawnRandomSphere()
        {
            if (spheres.Count == 0 && spawnPoints.Count == 0)
                return;

            int spawnPointsIndex = Random.Range(0, spawnPoints.Count);
            int sphereIndex = Random.Range(0, spheres.Count);
            Instantiate(spheres[sphereIndex], spawnPoints[spawnPointsIndex].position, Quaternion.identity);
        }
        #endregion
    }

}
