using JetBrains.Annotations;
using Mono.Cecil.Cil;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class strikerController : MonoBehaviour
{
    [SerializeField] private Slider theSlider;
    [SerializeField] private GameObject strikerDr;
    [SerializeField]
    private bool launched = false;
    private Rigidbody2D rb2d;
    private int strikerSpd = 150;
    [SerializeField]
    public bool onStrikerClick;
    private float timeElapsed = 0f;
    private const float TIME_THRESHOLD = 2f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }
    public void OnEnable()
    {
        transform.position =new Vector3 (0, -1.58f, 0);
        
    }
    private void Start()
    {
        theSlider.onValueChanged.AddListener(strikerXmove);
        strikerDr.SetActive(false);

        onStrikerClick = false;
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;

        Vector3 lookDir = mousePos - transform.position;
        lookDir.z = 0f;
        Quaternion rotation = Quaternion.LookRotation(Vector3.back, -lookDir);
        transform.rotation = rotation;

        float distance = Vector3.Distance(transform.position, mousePos);
        float scaleFactor = distance * 7f;
        scaleFactor = Mathf.Clamp(scaleFactor, 0f, 15f);
        strikerDr.transform.localScale = Vector3.one * scaleFactor;
        strikerSpd = Mathf.RoundToInt(scaleFactor * 700f);
    }

    private void strikerXmove(float value)
    {
        transform.position = new Vector3(value, transform.position.y, transform.position.z);
    }



    



    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && launched == false)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);


            if (hitCollider != null && hitCollider.CompareTag("striker"))
            {

                // The left mouse button is pressed down on an object with the "Striker" tag
                Debug.Log("Left mouse button pressed down on the striker.");
                strikerDr.SetActive(true);

                onStrikerClick = true;

            }
        }
        else if (Input.GetMouseButtonUp(0) && onStrikerClick)
        {
            strikerDr.SetActive(false);
            rb2d.AddForce(transform.up * strikerSpd);
            CarromRules.instance.forceAdded= true;
            onStrikerClick = false;
            Debug.Log("leftbuttonup");
            launched = true;
        }
        else if (rb2d.velocity.magnitude < 0.1f && launched)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= TIME_THRESHOLD)
            {
                Debug.Log("reposition");
                rb2d.velocity = Vector2.zero;
                transform.position = new Vector3(0, -1.58f, 0);
                launched = false;
                timeElapsed = 0f;
            }
        }
        else
        {
            timeElapsed = 0f;
        }




    }
}
