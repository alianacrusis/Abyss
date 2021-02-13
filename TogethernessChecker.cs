using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogethernessChecker : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        
    }
}
