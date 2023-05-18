using UnityEngine;

public class CarromGameManager : MonoBehaviour
{
    public static CarromGameManager instance;

    public SystemAI systemAI;
    public strikerController playerController;

    public bool ChangeTurn { get; set; }

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
        ChangeTurn = false;
        playerController.enabled = true;
        systemAI.enabled = false;
    }

    private void Update()
    {
        if (ChangeTurn)
        {
            ChangeTurn = false;

            if (playerController.enabled)
            {
                playerController.enabled = false;
                systemAI.enabled = true;
            }
            else if (systemAI.enabled)
            {
                systemAI.enabled = false;
                playerController.enabled = true;
            }
        }
    }
}
