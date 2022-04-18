
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    public Vector3 velocity;
    public Vector3 acceleration;
    public float speed = 1;
    public float magnitude;
    //public Transform target;
    public int nbRayon = 5;
    private float maxForce = 1f;
    private float maxSpeed = 2.7f;

    public List<Boid> neigbours;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        velocity = new Vector3(x, y, 0);
        acceleration = new Vector3(0, 0, 0);
        magnitude = Random.Range(0.5f, 1.5f);
        //speed = Random.Range(1f, 3f);
        neigbours = new List<Boid>();
        GetComponent<CircleCollider2D>().enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        SeperateNeigbour();
        AlignWithNeigbour();
        CohesionWithNeigbour();
        


        RayCasting();
        
        //GetComponent<Rigidbody2D>().velocity = velocity * speed;
        UpdateTransformation();
        UpdateRotationByVelocity();
        acceleration = Vector3.zero;
    }

    public void UpdateTransformation()
    {
        try
        {
            this.transform.position += (velocity * Time.deltaTime) * magnitude;
        }catch(System.Exception e)
        {
            Debug.Log(e.Message);
            this.transform.position = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        }
        
        //this.velocity.normalized()


        this.velocity += acceleration;
        //this.velocity = new Vector3(Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed));

    }


    public void UpdateRotationByVelocity()
    {
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * velocity;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * 500 * Time.deltaTime);
    }

/*    public void ChangeDirection(string nameWall)
    {
        if (nameWall == "Left" || nameWall == "Right")
        {
            if (velocity.y < 0)
            {
                velocity = new Vector2(-velocity.x  * 0.2f, -1 * Random.Range(-1f, 1f));
            }
            else
            {
                velocity = new Vector2(-velocity.x * 0.2f, Random.Range(-1f, 1f));
            }

        }
        if (nameWall == "Top" || nameWall == "Bottom")
        {
            if (velocity.x < 0)
            {
                velocity = new Vector2(-1 * Random.Range(-1f, 1f), -velocity.y * 0.2f);
            }
            else
            {
                velocity = new Vector2(Random.Range(-1f, 1f), -velocity.y * 0.2f);
            }

        }
    }*/
    public void ChangeDirection()
    {
        //Vector2 steering = new Vector2(Mathf.Clamp(Random.Range(-1f, 1f) * maxSpeed , -maxSpeed , maxSpeed), Mathf.Clamp(Random.Range(-1f, 1f) * maxSpeed, -maxSpeed,maxSpeed));
        //velocity = velocity.normalized * steering;
        //velocity  = new Vector2(   (Random.Range(-1f, 1f) * maxSpeed), (Random.Range(-1f,1f) * maxSpeed));

        velocity = new Vector3(Mathf.Clamp(Random.Range(-1f, 1f) * maxSpeed, -maxSpeed, maxSpeed), Mathf.Clamp(Random.Range(-1f, 1f) * maxSpeed, -maxSpeed, maxSpeed));
        //steerting *= maxSpeed;
        //this.acceleration = steering;


/*        Vector3 steerting = new Vector3(Mathf.Clamp(Random.Range(-1f, 1f) * maxSpeed, -maxSpeed, maxSpeed), Mathf.Clamp(Random.Range(-1f, 1f) * maxSpeed, -maxSpeed, maxSpeed));
        steerting = new Vector3(Mathf.Clamp(steerting.x - velocity.x, -maxForce, maxForce), Mathf.Clamp(steerting.y - velocity.y, -maxForce, maxForce));
        this.acceleration = steerting;*/

        //ChangeSpeedRandom();
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
            if (!neigbour)
            {
                return;
            }
            x_average = neigbour.velocity.x + x_average;
            y_average = neigbour.velocity.y + y_average;
        }
        x_average /= neigbours.Count;
        y_average /= neigbours.Count;


        Vector3 steerting = new Vector3(Mathf.Clamp(x_average * maxSpeed, -maxSpeed, maxSpeed),Mathf.Clamp( y_average * maxSpeed, -maxSpeed,maxSpeed)) ;
        steerting =   new Vector3(Mathf.Clamp(steerting.x - velocity.x, -maxForce , maxForce)  , Mathf.Clamp(  steerting.y - velocity.y, -maxForce, maxForce));
        ///steerting *= maxSpeed;
        ///
      
        this.acceleration += steerting;
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
            if (!neigbour)
            {
                return;
            }
            x_average = neigbour.transform.position.x + x_average;
            y_average = neigbour.transform.position.y + y_average;
        }
        x_average /= neigbours.Count;
        y_average /= neigbours.Count;

        //velocity =  new Vector3( velocity.x * (  x_average - transform.position.x)   , velocity.y *  (y_average - transform.position.y    ) );

        Vector3 steering = new Vector2(Mathf.Clamp( x_average - this.transform.position.x, -maxSpeed , maxSpeed), Mathf.Clamp(y_average - this.transform.position.y, -maxSpeed , maxSpeed));
        steering = new Vector3(Mathf.Clamp(steering.x - velocity.x, -maxForce, maxForce), Mathf.Clamp(steering.y - velocity.y, -maxForce, maxForce));
        acceleration +=  (steering / 1.2f);
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
            if (!neigbour)
            {
                return;
            }
            float invertProportionalX = (transform.position.x - neigbour.transform.position.x); /*/  ((transform.position.x - neigbour.transform.position.x) * (transform.position.x - neigbour.transform.position.x));*/
           
            x_average = invertProportionalX + x_average;
            float invertProportionalY = (transform.position.y - neigbour.transform.position.y); /*/ ((transform.position.y - neigbour.transform.position.y) * (transform.position.y - neigbour.transform.position.y));*/
           y_average = invertProportionalY + y_average;

        }
        x_average /= neigbours.Count;
        
        y_average /= neigbours.Count;
       

        Vector3 steering =  new Vector3(Mathf.Clamp( x_average * maxSpeed , -maxSpeed, maxSpeed) ,Mathf.Clamp( y_average * maxSpeed , -maxSpeed, maxSpeed));
        steering = new Vector3(Mathf.Clamp(steering.x - velocity.x, -maxForce, maxForce), Mathf.Clamp(steering.y - velocity.y, -maxForce, maxForce));
        acceleration +=  (steering / 2.3f);
        //direction = direction  - steer  ;
    }


    public void RayCasting()
    {

        RaycastHit2D ray = Physics2D.Raycast(this.transform.position , velocity , 0.8f);

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

    public IEnumerator SetPositionCouroutine(float position_x, float position_y )
    {
        yield return new WaitForSeconds(0.00001f);
        this.transform.position = new Vector2(position_x, position_y);
    }





}
