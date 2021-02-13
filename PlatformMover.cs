using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {

    [SerializeField] Transform targetA;
    [SerializeField] Transform targetB;

    [SerializeField] float platformSpeed = 2f;

    bool changingDirection = false;

    void Start()
    {

    }

    void FixedUpdate()
    {
        PingPongPlatform();
    }

    private void PingPongPlatform()
    {
        float step = platformSpeed * Time.deltaTime;

        if (changingDirection == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, step);
        }

        else if (changingDirection == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetA.position, step);
        }

        if (transform.position == targetB.position)
        {
            changingDirection = true;
        }

        else if (transform.position == targetA.position)
        {
            changingDirection = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Protagonist")
        {
            other.transform.parent = this.transform;
        }

        if (other.gameObject.tag == "Shadow")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Protagonist")
        {
            other.transform.parent = null;
        }

        if (other.gameObject.tag == "Shadow")
        {
            other.transform.parent = null;
        }
    }
}
