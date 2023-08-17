using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{

    public float lifeTime = 2.0f;

    public GameObject laserPrefab;
    public int initialPoolSize = 15;
    private Queue<GameObject> laserPool = new();
    GameObject lasersParent;


    // Start is called before the first frame update
    void Start()
    {
        // Crear un objeto padre para las instancias de láseres
        lasersParent = new GameObject("LasersParent");

        // Crear el pool inicial de láseres
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject laser = Instantiate(laserPrefab, lasersParent.transform);
            laser.SetActive(false);
            laserPool.Enqueue(laser);
        }
    }

    private void Update()
    {
        Debug.Log(laserPool.Count);
    }


    public void Shoot(Vector3 position, Quaternion rotation)
    {

        GameObject laser = laserPool.Dequeue();
        if (laser != null)
        {
            laser.transform.SetPositionAndRotation(position, rotation);
            LaserBehavior laserBehavior = laser.GetComponent<LaserBehavior>();
            laser.SetActive(true);

            laserPool.Enqueue(laser);
        }

    }



}
