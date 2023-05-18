using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class collecionHoder : MonoBehaviour
{
    public Score scorescript;
    public GameObject striker;
    public bool StrikerDestroyed = false;

    private void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("puck"))
        {
            Destroy(collider.gameObject);
            CarromRules.instance.PuckD = true;
            Debug.Log("puckd");
            Score.adding = true;
        }
        else if (collider.gameObject.CompareTag("striker"))
        {

            //CarromGameManager.instance.ChangeTurn = true;
            collider.attachedRigidbody.velocity = Vector2.zero;
            CarromRules.instance.StrikerD = true;

        }
    }
}


