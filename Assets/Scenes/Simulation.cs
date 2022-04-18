using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    public int nbBoid = 1;
    public GameObject boid;
    public GameObject objectInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(boid.GetComponent<Boid>().SetPositionCouroutine(500, 500));
        CreateBoids();
        //Destroy(boid);
    }

    // Update is called once per frame
    void Update()
    {
        GetClick();
    }


    public void CreateBoids()
    {
        for (int i = 0; i < nbBoid; i++)
        {
            Instantiate(boid);
            boid.GetComponent<Boid>().velocity = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }

    public void GetClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            {
                Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (pz.y < -3.6)
                {
                    return;
                }
                InstantiateWall( pz);
            }
        }
    }

    public void InstantiateWall(Vector3 position)
    {
        GameObject newObject = Instantiate(objectInstantiate, new Vector3(position.x, position.y, 0), objectInstantiate.transform.rotation);
        if(newObject.GetComponent<Boid>())
            StartCoroutine(newObject.GetComponent<Boid>().SetPositionCouroutine(position.x, position.y));
    }


    public void OnclickButton(GameObject paramObject)
    {
        objectInstantiate = paramObject;

    }
}
