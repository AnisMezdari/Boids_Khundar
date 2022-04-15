using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    public Vector3 direction;

    public int speed = 1;

    public Transform target;

    private bool canChangeDirection = true;

    private string lastWallTouch;

    // Start is called before the first frame update
    void Start()
    {
        //direction = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed; ;
        // rotate that vector by 90 degrees around the Z axis
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * direction ;

        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500 * Time.deltaTime);

        //transform.rotation = targetRotation ;


    }

    public void ChangeDirection(string nameWall)
    {
/*        if (!canChangeDirection)
        {
            return;
        }*/
        if (lastWallTouch != null)
        {
            if(lastWallTouch == nameWall)
            {
                return;
            }
        }
        if (nameWall == "Left" || nameWall == "Right")
        {
            if (direction.y < 0)
            {
                direction = new Vector2(-direction.x  * 0.2f, -1 * Random.Range(0.1f, 1f));
            }
            else
            {
                direction = new Vector2(-direction.x * 0.2f, Random.Range(0.1f, 1f));
            }

        }
        if (nameWall == "Top" || nameWall == "Bottom")
        {
            if (direction.x < 0)
            {
                direction = new Vector2(-1 * Random.Range(0.1f, 1f), -direction.y * 0.2f);
            }
            else
            {
                direction = new Vector2(Random.Range(0.1f, 1f), -direction.y * 0.2f);
            }

        }
        lastWallTouch = nameWall;
        canChangeDirection = false;
        //transform.Rotate(0, 0, (direction.x * 90  +  direction.y *90) );
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Wall"))
        {
            ChangeDirection(collision.name);
            //StartCoroutine(CoroutineCanChangeDirection());
        }
    }

    /*    private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag.Equals("Wall"))
            {
                ChangeDirection(collision.name);
            }
        }*/
/*    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Wall"))
        {
            ChangeDirection(collision.name);
            //StartCoroutine(CoroutineCanChangeDirection());
        }
    }*/


    public IEnumerator CoroutineCanChangeDirection()
    {
        yield return new WaitForSeconds(0.05f);
        canChangeDirection = true;
    }

}
