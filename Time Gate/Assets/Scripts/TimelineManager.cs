using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//currently this just fixes the movement after cutscene for player.
public class TimelineManager : MonoBehaviour
{
    private bool fix = false;
    public Animator playerAnimator;
    public RuntimeAnimatorController playerAnim;
    public PlayableDirector director;
    // Start is called before the first frame update
    void OnEnable() //this is for trigger zones. Could also just change to Start()
    {
        playerAnim = playerAnimator.runtimeAnimatorController;
        playerAnimator.runtimeAnimatorController = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(director.state != PlayState.Playing && !fix)
        {
            fix = true;
            playerAnimator.runtimeAnimatorController = playerAnim;
        }
    }
}
