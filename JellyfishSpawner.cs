using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishSpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] Jellyfish[] jellyPrefabArray;
    bool spawn;

    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnJellies();
        }
    }

    private void Awake()
    {
        spawn = true;
    }

    public void StopSpawning()
    {
        Debug.Log("StopSpawning() has been called!");
        spawn = false;
    }

    private void SpawnJellies()
    {
        var jellyIndex = UnityEngine.Random.Range(0, jellyPrefabArray.Length);
        Spawn(jellyPrefabArray[jellyIndex]);
    }

    private void Spawn(Jellyfish myJellies)
    {
        Jellyfish newSeed = Instantiate(myJellies, transform.position, transform.rotation) as Jellyfish;
    }
}
