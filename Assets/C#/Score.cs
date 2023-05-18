using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public SystemAI SystemScript;
    public strikerController playerScript;
    public Text playertext;
    public Text SYSscoretext;
    public static int PlayerScorenum;
    public static int SYSScorenum;
    public GameObject[] pucks;
    public static bool adding;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScorenum = 0;
        SYSScorenum = 0;
        playertext.text = "player score: " + PlayerScorenum;
        SYSscoretext.text = "system score: " + SYSScorenum;
    }

    // Update is called once per frame
    void Update()
    {
        pucks = GameObject.FindGameObjectsWithTag("puck");
        int PucksOnBoard = pucks.Length;
        if (adding)
        {
            
            adding = false;
            if (playerScript.enabled)
            {

                PlayerScorenum++;
                playertext.text = "player score: " + PlayerScorenum;
            }
            else if(SystemScript.enabled) 
            {
                SYSScorenum++;
                SYSscoretext.text = "System score: " + SYSScorenum;
            }
        }
        
    }
}
