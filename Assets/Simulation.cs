using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    public int nbBoid = 20;
    public GameObject boid;

    // Start is called before the first frame update
    void Start()
    {
        CreateBoids();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateBoids()
    {
        for (int i = 0; i < 20; i++)
        {
            Instantiate(boid);
            boid.GetComponent<Boid>().direction = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}
