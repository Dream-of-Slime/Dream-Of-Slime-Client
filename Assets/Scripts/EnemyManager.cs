using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slime
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager instance;

        [SerializeField] List<GameObject> EnemySamples;
        List<List<GameObject>> EnemyPools;

        [SerializeField] Transform EnemyParent;

        [SerializeField] float _spawnTime;

        int SpawnOffset = 100;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            EnemyPools = new List<List<GameObject>>();

            for (int i = 0; i < EnemySamples.Count; i++)
            {
                EnemyGenerate(i, 100);
            }

            StartCoroutine("GenerateTest");
        }

        //void Update()
        //{
            
        //}

        IEnumerator GenerateTest()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnTime);
                EnemyActive(0, 1);
            }
        }

        public void EnemyGenerate(int enemy, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject temp = Instantiate(EnemySamples[enemy], EnemyParent);
                temp.SetActive(false);
                while (EnemyPools.Count <= enemy)
                {
                    EnemyPools.Add(new List<GameObject>());
                }
                EnemyPools[enemy].Add(temp);
            }
        }

        public void EnemyActive(int enemy, int amount)
        {
            int actived = 0;
            for (int i = 0; i < EnemyPools[enemy].Count; i++)
            {
                if (!EnemyPools[enemy][i].activeSelf)
                {
                    int random = Random.Range(0, 3);
                    int randomX = 0;
                    int randomY = 0;

                    if (random == 0)
                    {
                        randomX = -SpawnOffset;
                        randomY = Random.Range(-SpawnOffset, SpawnOffset + ResolutionManager.ResolutionY);
                    }
                    else if (random == 1)
                    {
                        randomX = Random.Range(-SpawnOffset, SpawnOffset + ResolutionManager.ResolutionX);
                        randomY = SpawnOffset + ResolutionManager.ResolutionY;
                    }
                    else if (random == 2)
                    {
                        randomX = SpawnOffset + ResolutionManager.ResolutionX;
                        randomY = Random.Range(-SpawnOffset, SpawnOffset + ResolutionManager.ResolutionY);
                    }
                    else if (random == 3)
                    {
                        randomX = Random.Range(-SpawnOffset, SpawnOffset + ResolutionManager.ResolutionX);
                        randomY = -SpawnOffset;
                    }

                    EnemyPools[enemy][i].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(randomX, randomY, 0));
                    EnemyPools[enemy][i].SetActive(true);
                    actived++;
                    if (actived >= amount)
                    {
                        return;
                    }
                }
            }

            if (actived < amount)
            {
                EnemyGenerate(enemy, amount - actived);
                EnemyActive(enemy, amount - actived);
            }
        }
    }
}