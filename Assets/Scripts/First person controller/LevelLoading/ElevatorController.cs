using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    // Ready to leave should be used to indicate elevator that puzzle was solved and doors should open.
    public bool startReadyToLeaveSequence = false;
    public bool startLevelDoneSequence = false;
    public bool startLevelLoadedSequence = false;

    private bool levelDoneSequenceAlreadyStarted = false;
    private bool readyToLeaveSequencAlreadyStarted = false;
    private bool levelLoadedSequenceAlreadyStarted = false;

    public Animator animator;

    public bool isExitElevator = true;

    [SerializeField]
    private float timeToWaitBeforeLevelDoneSequence = 1000;
    private float time;

    public GameObject player;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isExitElevator)
        {
            if (startLevelDoneSequence && Time.realtimeSinceStartup - time > timeToWaitBeforeLevelDoneSequence && !levelDoneSequenceAlreadyStarted)
            {
                player.GetComponent<Rigidbody>().isKinematic = true;
                player.transform.SetParent(gameObject.transform);
                player.GetComponent<FirstPersonMovement>().dontMove = true;
                animator.SetTrigger("isLevelDone");
                GameObject.FindObjectOfType<SceneLoader>().LoadNextScene();
                levelDoneSequenceAlreadyStarted = true;
            }
            if (startReadyToLeaveSequence && !readyToLeaveSequencAlreadyStarted)
            {
                animator.SetTrigger("isPlayerReadyToLeave");
                readyToLeaveSequencAlreadyStarted = true;
            }
            if (startLevelLoadedSequence && !levelLoadedSequenceAlreadyStarted)
            {
                animator.SetTrigger("isLevelLoaded");
                levelLoadedSequenceAlreadyStarted = true;
            }
        }
    }

    public void OnLevelLoadedAnimationEnd()
    {
        //player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<FirstPersonMovement>().levelDone = true;
        player.GetComponent<FirstPersonMovement>().dontMove = false;
        isExitElevator = false;
        //dontDestroyList[dontDestroyList.Count - 1].GetComponent<ElevatorController>().isExitElevator = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (isExitElevator)
        {
            if (collider.gameObject.tag == "Player")
            {
                startLevelDoneSequence = true;
                time = Time.realtimeSinceStartup;
            }
        }
    }
}
