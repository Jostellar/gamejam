using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatManager : MonoBehaviour
{
    public GameObject[] floatPrefabs;
    public int maxFloats = 4;
    public float spawnDelayMin = 1f;
    public float spawnDelayMax = 3f;

    public float startX = 0f;
    public float endX = 10f;

    public float spawnY = 0f;
    public float spawnZ = 20f;

    private List<GameObject> activeFloats = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnFloatsRoutine());
    }

    IEnumerator SpawnFloatsRoutine()
    {
        while (true)
        {
            if (activeFloats.Count < maxFloats)
            {
                SpawnRandomFloat();
            }

            float delay = Random.Range(spawnDelayMin, spawnDelayMax);
            yield return new WaitForSeconds(delay);
        }
    }

    void SpawnRandomFloat()
    {
        if (floatPrefabs == null || floatPrefabs.Length == 0)
        {
            Debug.LogError("Ingen float-prefabs er tilføjet til FloatManager på " + gameObject.name);
            return;
        }

        int index = Random.Range(0, floatPrefabs.Length);
        GameObject floater = Instantiate(floatPrefabs[index]);

        Vector3 spawnPos = new Vector3(startX, spawnY, spawnZ);
        floater.transform.position = spawnPos;

        // Konfigurer FloaterMover
        FloaterMover mover = floater.GetComponent<FloaterMover>();
        if (mover != null)
        {
            mover.endX = endX;
            mover.manager = this;
        }

        activeFloats.Add(floater);
    }

    public void NotifyFloatReachedEnd(GameObject floater)
    {
        if (activeFloats.Contains(floater))
        {
            activeFloats.Remove(floater);
            Destroy(floater);
        }
    }
}

