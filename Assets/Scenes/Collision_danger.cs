using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_danger : MonoBehaviour
{

    private bool canChangeDirection = true;
    private string lastWallTouch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeOppositeDirection(string nameWall)
    {
        if (!canChangeDirection)
        {
            return;
        }
        transform.parent.GetComponent<Boid>().direction = new Vector2(-transform.parent.GetComponent<Boid>().direction.x, -transform.parent.GetComponent<Boid>().direction.y);
        canChangeDirection = false;

    }

    public void ChangeDirection(string nameWall)
    {
        /*        if (!canChangeDirection)
                {
                    return;
                }*/
        if (lastWallTouch != null)
        {
            if (lastWallTouch == nameWall)
            {
                return;
            }
        }
        if (nameWall == "Left" || nameWall == "Right")
        {
            if (transform.parent.GetComponent<Boid>().direction.y < 0)
            {
                transform.parent.GetComponent<Boid>().direction = new Vector2(-transform.parent.GetComponent<Boid>().direction.x * 0.2f, -1 * Random.Range(-1f, 1f));
            }
            else
            {
                transform.parent.GetComponent<Boid>().direction = new Vector2(-transform.parent.GetComponent<Boid>().direction.x * 0.2f, Random.Range(-1f, 1f));
            }

        }
        if (nameWall == "Top" || nameWall == "Bottom")
        {
            if (transform.parent.GetComponent<Boid>().direction.x < 0)
            {
                transform.parent.GetComponent<Boid>().direction = new Vector2(-1 * Random.Range(-1f, 1f), -transform.parent.GetComponent<Boid>().direction.y * 0.2f);
            }
            else
            {
                transform.parent.GetComponent<Boid>().direction = new Vector2(Random.Range(-1f, 1f), -transform.parent.GetComponent<Boid>().direction.y * 0.2f);
            }

        }
        lastWallTouch = nameWall;

        //transform.Rotate(0, 0, (direction.x * 90  +  direction.y *90) );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("Wall"))
        {
            ChangeOppositeDirection(collision.name);
            //StartCoroutine(CoroutineCanChangeDirection());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Wall"))
        {
            ChangeDirection(collision.name);
            StartCoroutine(CoroutineCanChangeDirection());
        }
    }


    public IEnumerator CoroutineCanChangeDirection()
    {
        yield return new WaitForSeconds(0.5f);
        canChangeDirection = true;
    }
}
