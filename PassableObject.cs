using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassableObject : MonoBehaviour
{ 
    CharacterManager charManager;
    SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        charManager = FindObjectOfType<CharacterManager>();
        _renderer = GameObject.Find("Shadow").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (charManager.protagonistIsActive == true)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if (charManager.shadowIsActive == true)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.tag == "Shadow")
        {
            Color temp = _renderer.color;
            temp.a = 0.8f;
            _renderer.color = temp;
        }
    }
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.tag == "Shadow")
        {
            Color temp = _renderer.color;
            temp.a = 1f;
            _renderer.color = temp;
        }
    }


}
