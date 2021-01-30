using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public List<string> sceneList = new List<string>();
    int currentScene = 0;

    List<GameObject> dontDestroyList = new List<GameObject>();
    bool firstRun = true;
    public GameObject firstElevator;
    public GameObject player;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(player);
        if (firstRun)
        {
            dontDestroyList.Add(firstElevator);
        }

        foreach (GameObject elevator in dontDestroyList)
        {
            DontDestroyOnLoad(elevator);
        }

    }

    private void OnSceneLoad()
    {
        if (dontDestroyList.Count > 0 && !firstRun)
        {
            Destroy(dontDestroyList[0]);
            dontDestroyList.RemoveAt(0);
            foreach (GameObject elevator in GameObject.FindGameObjectsWithTag("Elevator"))
            {
                bool exists = false;
                foreach (GameObject dontDestroyObject in dontDestroyList)
                {
                    if (elevator == dontDestroyObject)
                    {
                        exists = true;
                    }
                }
                if (!exists)
                {
                    dontDestroyList.Add(elevator);
                }
            }
        }
        else
        {
            foreach (GameObject elevator in GameObject.FindGameObjectsWithTag("Elevator"))
            {
                dontDestroyList.Add(elevator);
            }
        }
        firstRun = false;

        if (!firstRun)
        {
            foreach (GameObject elevator in dontDestroyList)
            {
                DontDestroyOnLoad(elevator);
            }
        }

        dontDestroyList[dontDestroyList.Count - 1].GetComponent<ElevatorController>().startLevelDoneSequence = false;
        dontDestroyList[dontDestroyList.Count - 1].GetComponent<ElevatorController>().startReadyToLeaveSequence = false;
        dontDestroyList[dontDestroyList.Count - 1].GetComponent<ElevatorController>().startLevelLoadedSequence = false;
        dontDestroyList[dontDestroyList.Count - 1].GetComponent<ElevatorController>().player = player;

        if (firstRun)
        {
            firstRun = false;
        }
    }

    // Start is called before the first frame update
    public void LoadNextScene()
    {
        if (currentScene + 1 < sceneList.Count)
        {
            StartCoroutine(LoadAsyncScene());
        }
        else
        {
            Debug.Log("!!!OUT OF SCENES!!!"); //TODO: replace with "game completed" menu scene.
        }
    }

    public void ReloadScene()
    {
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneList[currentScene]);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        foreach(GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (pl != player)
            {
                Destroy(pl);
            }
        }

        foreach (GameObject sm in GameObject.FindGameObjectsWithTag("SceneManager"))
        {
            if (sm != gameObject)
            {
                Destroy(sm);
            }
        }

        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<FirstPersonMovement>().levelDone = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        OnSceneLoad();
    }

    IEnumerator LoadAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneList[currentScene + 1]);

        currentScene++;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        foreach(GameObject elevator in GameObject.FindGameObjectsWithTag("Elevator"))
        {
            elevator.GetComponent<ElevatorController>().startLevelLoadedSequence = true;
        }

        OnSceneLoad();
    }
}
