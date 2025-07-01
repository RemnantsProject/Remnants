using UnityEngine;

namespace Remnants
{
    public class SpawnTrigger : MonoBehaviour
    {
        public bool isFinalTrigger = false;

        [SerializeField] private ObjectsSpawnManager manager;

        private void Awake()
        {
            if (manager == null)
            {
                manager = FindFirstObjectByType<ObjectsSpawnManager>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && manager != null)
            {
                if (!isFinalTrigger)
                {
                    manager.StartSpawning();
                }
                else
                {
                    manager.StopAndSpawnFinal();
                }

                GetComponent<Collider>().enabled = false;
            }
        }
    }
}
