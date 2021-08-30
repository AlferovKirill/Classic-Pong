using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Particles : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject particle;

    void Start()
    {
        Spawn();
    }

    
    void Spawn()
    {
        int rand = Random.Range(0, objects.Length - 1);
        float rand_y = Random.Range(-10f, 10f);

        objects[rand].transform.position = new Vector2(-20f, rand_y);
        particle = Instantiate(objects[rand], objects[rand].transform.position, Quaternion.identity) as GameObject;
        Invoke("Spawn", 0.5f);
    }
}


