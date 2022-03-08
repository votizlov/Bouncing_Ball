using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private GameObject[] prefabsToSpawn;
        [SerializeField] private float spawnProbability;

        public void RefreshSpawn()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                for (int i = 0; i < spawnPoint.childCount; i++)
                {
                    Destroy(spawnPoint.GetChild(i).gameObject);
                }

            }
            foreach (var tr in spawnPoints)
            {
                if (Random.Range(0f, 1f) > spawnProbability)
                {
                    continue;
                }
                var t = Instantiate(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)], tr.position,Quaternion.identity);
                t.transform.SetParent(tr,true);
            }
        }

        private void OnEnable()
        {
            RefreshSpawn();
        }
    }
}