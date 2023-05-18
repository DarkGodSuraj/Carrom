using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : MonoBehaviour
{
    public SystemAI systemscript;
    public strikerController playerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        if (systemscript.enabled)
        {
            Score.SYSScorenum++;
        }
        else if(playerScript.enabled)
        {
            Score.PlayerScorenum++;
        }
    }
}