using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SystemAI : MonoBehaviour
{
    public static SystemAI instance;
    public GameObject strikerDR;
    public List<Transform> pockets;
    public GameObject striker;
    [SerializeField]
    public GameObject[] pucks;

    public float maxForceMagnitude = 1000f;
    public float minDistanceToPocket = 1f;

    private Rigidbody2D strikerRb;
    private Vector3 startPos;
    [SerializeField]
    public static bool addForceEnabled;
    
    



    

    private void OnEnable()
    {
        
        transform.position = new Vector3(0f, 1.58f, 0f);
        addForceEnabled = true;
        
    }

    private void Start()
    {
        

        CarromRules.instance.desiredPos = new Vector3(0, 1.58f, 0);
        strikerRb = striker.GetComponent<Rigidbody2D>();
        startPos = striker.transform.position;



        
        addForceEnabled = true;
        strikerDR.SetActive(false);
    }
    

    private void Update()
    {
        GameObject[] puckObjects = GameObject.FindGameObjectsWithTag("puck");
        pucks = new GameObject[puckObjects.Length];
        for (int i = 0; i < puckObjects.Length; i++)
        {
            pucks[i] = puckObjects[i];
        }

        Transform targetPocket = FindTargetPocket();
        

        if (targetPocket != null && addForceEnabled == true)
        {
            Vector2 distanceToPocket = targetPocket.position - striker.transform.position;
            float angleToPocket = Vector2.SignedAngle(Vector2.up, distanceToPocket);
            Debug.Log("systemai before force");

            if (addForceEnabled && distanceToPocket.magnitude > minDistanceToPocket)
            {
                float forceMagnitude = Mathf.Clamp(distanceToPocket.magnitude, 0, minDistanceToPocket) * maxForceMagnitude;
                strikerRb.AddForce(distanceToPocket.normalized * forceMagnitude);
                Debug.Log("forceadded1");
                addForceEnabled = false;
                


                CarromRules.instance.forceAdded = true;
                
                

            }
        }
        else
        {
            
            Transform nearestPuck = FindNearestPuck();
            if (nearestPuck != null && addForceEnabled ==true )
            {
                Vector2 directionToPuck = (nearestPuck.position - striker.transform.position).normalized;
                if (addForceEnabled)
                {
                    strikerRb.AddForce(directionToPuck * maxForceMagnitude);
                    Debug.Log("forceadded2");
                    
                    addForceEnabled = false;
                    CarromRules.instance.forceAdded = true;
                }
            }
        }

        
        
    }

    private Transform FindTargetPocket()
    {
        
        Transform targetPocket = null;
        int minPucksInMiddle = int.MaxValue;

        foreach (Transform pocket in pockets)
        {
            
            Vector2 strikerPosition = striker.transform.position;
            Vector2 pocketPosition = pocket.position;
            int pucksInMiddle = CountPucksInMiddle(strikerPosition, pocketPosition);

            if (pucksInMiddle < minPucksInMiddle && pucksInMiddle >= 1)
            {
                minPucksInMiddle = pucksInMiddle;
                targetPocket = pocket;
                
                
            }
        }

        return targetPocket;
    }

    private Transform FindNearestPuck()
    {
        
        Transform nearestPuck = null;
        float minDistance = float.MaxValue;

        foreach (GameObject puck in pucks)
        {
            float distance = Vector2.Distance(striker.transform.position, puck.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPuck = puck.transform;
                
            }
        }

        return nearestPuck;
    }

    private int CountPucksInMiddle(Vector2 start, Vector2 end)
    {
        
        int count = 0;
        float distance = Vector2.Distance(start, end);
        Vector2 direction = (end - start).normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(start, direction, distance);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];

            if (hit.collider.gameObject.CompareTag("puck"))
            {
                Vector2 perpendicular = new Vector2(-direction.y, direction.x);
                float perpendicularDistance = Vector2.Dot(hit.point - start, perpendicular);
                if (Mathf.Abs(perpendicularDistance) < 0.5f)
                {
                    count++;
                }
            }
        }

        return count;
    }

    
}