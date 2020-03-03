using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnInitialGlobe : MonoBehaviour
{
    [SerializeField] // By using Serialize Field, we can change this value in the editor.
    bool InDebug = false;

    [SerializeField]
    bool PlaneDetection = true; // Allows us to switch between plane detection and trigger image forms of AR

    [Space] // The space decorator allows us to tidy up our editor component

    [SerializeField]
    GameObject PrefabToSpawn;

    [HideInInspector] // This decorator is only used when we want to have a public variable that can't be changed in editor. This usually comes from a coupling problem in the code.
    public bool Spawned = false;

    GameObject _spawnedObj;

    private void Start()
    {
        if (!InDebug) // If we aren't debugging the program, quickly clean up the scene before doing anything.
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("AR Object"))
                Destroy(g);
        }

        if (PlaneDetection)
            Destroy(this.GetComponent<ARTrackedImageManager>());
        if (!PlaneDetection)
            Destroy(this.GetComponent<ARPlaneManager>());
    }

    void Update()
	{
		if (!InDebug && PlaneDetection) // This is a failsafe so that we can't work on device if we've been debugging and haven't cleaned up the scene.
                                        // We do this because random objects in the scene can lead to issues unrelated to the regular application flow.
		{
			if (!Spawned && Input.touchCount > 0)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began) // We could probably move this to the Touch Response class but keeping it here means a more robust decoupled app. Good for maintenance.
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit, 10f))
					{
						this.GetComponent<ARPlaneManager>().enabled = false; // Always remember to remove the plane if using plane detection.
						foreach (GameObject g in GameObject.FindGameObjectsWithTag("AR Plane"))
							Destroy(g);
						_spawnedObj = Instantiate(PrefabToSpawn, hit.point, transform.rotation);
						Spawned = true;
					}
				}
			}
		}
		else
			StartCoroutine(InitSpawnedObj());
	}

	IEnumerator InitSpawnedObj() // You'll see this method throughout the app. This is one of those utility pieces of code i use when debugging or when linking to spawned AR content
	{
		while (!_spawnedObj)
		{
			if (GameObject.FindGameObjectWithTag("AR Object") != null)
			{
				_spawnedObj = GameObject.FindGameObjectWithTag("AR Object");
				Spawned = true;
			}
			else
				yield return new WaitForEndOfFrame();
		}
	}
}
