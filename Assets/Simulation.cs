using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    public int nbBoid = 1;
    public GameObject boid;

    // Start is called before the first frame update
    void Start()
    {
        CreateBoids();
        Destroy(boid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateBoids()
    {
        for (int i = 0; i < nbBoid; i++)
        {
            Instantiate(boid);
            boid.GetComponent<Boid>().direction = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}
