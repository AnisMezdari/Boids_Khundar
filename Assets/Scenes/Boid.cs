using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    public Vector3 direction;
    public float speed = 1;
    //public Transform target;
    public int nbRayon = 5;

    public List<Boid> neigbours;

    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        direction = new Vector3(x, y, 0);
        //speed = Random.Range(1f, 3f);
        neigbours = new List<Boid>();
    }

    // Update is called once per frame
    void Update()
    {






        CohesionWithNeigbour();
        AlignWithNeigbour();
      
        //SeperateNeigbour();
       

        RayCasting();
        UpdateRotationByVelocity();
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
    }


    public void UpdateRotationByVelocity()
    {
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * direction;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * 500 * Time.deltaTime);
    }

    public void ChangeDirection(string nameWall)
    {
        if (nameWall == "Left" || nameWall == "Right")
        {
            if (direction.y < 0)
            {
                direction = new Vector2(-direction.x  * 0.2f, -1 * Random.Range(-1f, 1f));
            }
            else
            {
                direction = new Vector2(-direction.x * 0.2f, Random.Range(-1f, 1f));
            }

        }
        if (nameWall == "Top" || nameWall == "Bottom")
        {
            if (direction.x < 0)
            {
                direction = new Vector2(-1 * Random.Range(-1f, 1f), -direction.y * 0.2f);
            }
            else
            {
                direction = new Vector2(Random.Range(-1f, 1f), -direction.y * 0.2f);
            }

        }
    }
    public void ChangeDirection()
    {
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        ChangeSpeedRandom();
    }

    public void ChangeSpeedRandom()
    {
        //yield return new WaitForSeconds(Random.Range(1, 5));
        speed = Random.Range(1f, 3f);
    }


    public void AlignWithNeigbour()
    {
        if(neigbours.Count == 0)
        {
            return;
        }
        float x_average = 0;
        float y_average = 0;
        foreach(Boid neigbour in neigbours)
        {
            x_average = neigbour.direction.x + x_average;
            y_average = neigbour.direction.y + y_average;
        }
        x_average /= neigbours.Count;
        y_average /= neigbours.Count;

        direction =   new Vector3(x_average, y_average);
        //direction = direction  - steer  ;
    }
    public void CohesionWithNeigbour()
    {
        if (neigbours.Count == 0)
        {
            return;
        }
        float x_average = 0;
        float y_average = 0;
        foreach (Boid neigbour in neigbours)
        {
            x_average = neigbour.transform.position.x + x_average;
            y_average = neigbour.transform.position.y + y_average;
        }
        x_average /= neigbours.Count;
        y_average /= neigbours.Count;

        direction =  new Vector3((  x_average - transform.position.x)   ,  (y_average - transform.position.y    ) );
        //direction = direction  - steer  ;
    }

    public void SeperateNeigbour()
    {
        if (neigbours.Count == 0)
        {
            return;
        }
        float x_average = 0;
        float y_average = 0;
        foreach (Boid neigbour in neigbours)
        {
            x_average = (transform.position.x - neigbour.transform.position.x) + x_average;
            y_average = (transform.position.y - neigbour.transform.position.y) + y_average;
        }
        x_average /= neigbours.Count;
        y_average /= neigbours.Count;

        direction =  new Vector3( x_average , y_average);
        //direction = direction  - steer  ;
    }


    public void RayCasting()
    {

        RaycastHit2D ray = Physics2D.Raycast(this.transform.position , direction , 1.5f);

        //Debug.Log(ray.point);
        if (!ray.collider)
        {
            return;
        }
        if (ray.collider.tag.Equals("Wall"))
        {
            //ChangeDirection(ray.collider.name);
            ChangeDirection();
        }
  
       
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Boid"))
        {
            neigbours.Add(collision.GetComponent<Boid>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Boid"))
        {
            neigbours.Remove(collision.GetComponent<Boid>());
        }
    }





}
