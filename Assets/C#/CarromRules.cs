using UnityEngine;
using System.Collections;

public class CarromRules : MonoBehaviour
{
    public static CarromRules instance;
    public SystemAI targetscript;
    [SerializeField]
    public bool forceAdded;

    [SerializeField]
    public bool puckD;

    [SerializeField]
    public bool strikerD;

    public bool ForceAdded
    {
        get { return forceAdded; }
        set { forceAdded = value; }
    }

    public bool PuckD
    {
        get { return puckD; }
        set { puckD = value; }
    }

    public bool StrikerD
    {
        get { return strikerD; }
        set { strikerD = value; }
    }

    public GameObject Rb;
    
    public Vector3 desiredPos;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        forceAdded = false;
        Rb = GameObject.FindGameObjectWithTag("striker");
        
    }
    private void Update()
    {
        
    }
    private void LateUpdate()
    {

        GameObject targetObject = GameObject.Find("striker");
        if (targetObject != null)
        {
            // Get the TargetScript component from the targetObject
            targetscript = targetObject.GetComponent<SystemAI>();

            if (targetscript == null)
            {
                Debug.LogError("TargetScript component not found on the targetObject.");
            }
        }
        else
        {
            Debug.LogError("TargetObject not found in the scene.");
        }

        if (forceAdded)
        {

            forceAdded = false;

            StartCoroutine(DelayedLateUpdate(3f));

        }
        IEnumerator DelayedLateUpdate(float delayTime)
        {
            yield return new WaitForSeconds(3f);



            forceAdded = false;
            Debug.Log("Before slow");
            if (puckD && !strikerD)
            {
                
                Rb.transform.position = desiredPos;
                
                puckD = false;
                Debug.Log("Puck count decreased. Shoot again!");

                if (targetscript != null && targetscript.enabled )
                {
                    Debug.Log("null ref");

                    SystemAI.addForceEnabled = true;

                   
                }
            
            }
            else if (strikerD && !puckD)
            {
                strikerD = false;
                CarromGameManager.instance.ChangeTurn = true;

                Debug.Log("Opposite");

            }
            else if (strikerD && puckD)
            {
                puckD = false;
                strikerD = false;
                Debug.Log("Both D");
                CarromGameManager.instance.ChangeTurn = true;

            }
            else if (!strikerD && !puckD)
            {
                Debug.Log("Both not");
                CarromGameManager.instance.ChangeTurn = true;

            }
        }
    }


}