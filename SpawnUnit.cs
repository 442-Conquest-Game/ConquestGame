using UnityEngine;
using System.Collections;

public class SpawnUnit : MonoBehaviour
{
    float currentCount = 0;
    public Vector3 spawnValues;
    public float maxUnitCount;
    public float spawnTime;
    public GameObject unit;
    public Transform spawnPoint;

    void Start()
    {
        StartCoroutine(Spawn(unit));
    }
    
    IEnumerator Spawn(GameObject unit)
    {
            while (currentCount < maxUnitCount)
            {
                print(unit);
                if (unit.transform.position == spawnPoint.position)
                {
                    spawnValues = new Vector3(spawnPoint.position.x, 0, spawnPoint.position.z + 5);
                    spawnPoint.position = spawnValues;
                }
                else if (spawnPoint.position.z != 15)
                {
                    spawnValues = new Vector3(spawnPoint.position.x, 0, spawnPoint.position.z - 5);
                }
                Instantiate(unit, spawnPoint.position, spawnPoint.rotation);
                currentCount++;
                print(spawnPoint.position);
                yield return new WaitForSeconds(spawnTime);
            }
     }
}