using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] Bubble[] bubblePrefabArray;
    public bool spawn;

    private void Awake()
    {
        spawn = false;
        Debug.Log("Bubble spawner not active");
    }

    IEnumerator SpawningBubbles()
    {
        yield return new WaitForSeconds(5f);

        while (spawn == true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnBubbles();
        }
    }

    public void ActiveSpawner()
    {
        spawn = true;
        StartCoroutine(SpawningBubbles());
    }

    private void SpawnBubbles()
    {
        var bubbleIndex = UnityEngine.Random.Range(0, bubblePrefabArray.Length);
        Spawn(bubblePrefabArray[bubbleIndex]);
    }

    private void Spawn(Bubble myBubbles)
    {
        Vector3 instantiationPt = new Vector3(transform.position.x, transform.position.y, 0);
        Bubble newBubble = Instantiate(myBubbles, instantiationPt, Quaternion.identity) as Bubble;
    }
}
