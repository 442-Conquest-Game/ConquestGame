using UnityEngine;
using System.Collections;

public class SpawnUnit : MonoBehaviour
{
    float currentCount;
    public float maxUnitCount;
    public float spawnTime;
    public GameObject unit;
    public Collider castleBoundaries;

    void Awake()
    {
        castleBoundaries = GetComponent<Collider>();
    }
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
    void Spawn()
    {
        if(currentCount >= maxUnitCount)
        {
            return;
        }
        Instantiate(unit, castle.position, castle.rotation);
        currentCount++;
    }
}