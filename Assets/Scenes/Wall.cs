using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    private bool itCanDestroy;
    // Start is called before the first frame update
    void Start()
    {
        itCanDestroy = true;
        StartCoroutine(ItCanDestroyCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Boid" && this.itCanDestroy)
        {
            Destroy(collision.gameObject);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boid" && this.itCanDestroy)
        {
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator ItCanDestroyCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        itCanDestroy = false;
    }
}
