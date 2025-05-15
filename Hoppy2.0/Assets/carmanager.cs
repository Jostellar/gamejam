using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject[] carPrefabs;    // Forskellige bil-prefabs
    public int maxCars = 4;
    public float spawnDelayMin = 1f;
    public float spawnDelayMax = 3f;

    public float startX = -37f;
    public float endX = 28f;

    public float spawnY = 0f; // Tilpas højden for denne vej
    public float spawnZ = 0f; // Tilpas dybden for denne vej

    private List<GameObject> activeCars = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnCarsRoutine());
    }

    IEnumerator SpawnCarsRoutine()
    {
        while (true)
        {
            if (activeCars.Count < maxCars)
            {
                SpawnRandomCar();
            }

            float delay = Random.Range(spawnDelayMin, spawnDelayMax);
            yield return new WaitForSeconds(delay);
        }
    }

    void SpawnRandomCar()
    {
        if (carPrefabs == null || carPrefabs.Length == 0)
        {
            Debug.LogError("Ingen bil-prefabs er tilføjet til CarManager på " + gameObject.name);
            return;
        }

        int index = Random.Range(0, carPrefabs.Length);
        GameObject car = Instantiate(carPrefabs[index]);

        // Brug spawnY og spawnZ til at placere bilen korrekt på vejen
        Vector3 spawnPos = new Vector3(startX, spawnY, spawnZ);
        car.transform.position = spawnPos;

        // Konfigurer CarMover
        CarMover mover = car.GetComponent<CarMover>();
        if (mover != null)
        {
            mover.endX = endX;
            mover.manager = this;
        }

        activeCars.Add(car);
    }

    public void NotifyCarReachedEnd(GameObject car)
    {
        if (activeCars.Contains(car))
        {
            activeCars.Remove(car);
            Destroy(car);
        }
    }
}
