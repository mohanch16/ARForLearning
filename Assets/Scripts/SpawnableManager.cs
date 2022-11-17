using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnableManager : MonoBehaviour
{
    [SerializeField]
    public ARRaycastManager m_RaycastManager;

    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    [SerializeField]
    public GameObject spawnablePrefab;

    public bool allowMultipleObjects = true;

    public GameObject[] animationControls;

    Camera arCam;
    GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        this.spawnedObject = null;
        this.arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) 
        {
            return; 
        }

        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (this.m_RaycastManager.Raycast(Input.GetTouch(0).position, this.m_Hits)) 
        {
            if (Input.GetTouch(0). phase == TouchPhase.Began && this.spawnedObject == null) 
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Spawnable")
                    {
                        this.spawnedObject = hit.collider.gameObject;                        
                    }
                    else
                    {
                        this.SpawnPrefab(this.m_Hits[0].pose.position);     
                        
                        // activating animation controls
                        if (this.animationControls != null && this.animationControls.Length > 0) 
                        {
                            for (int index = 0; index < this.animationControls.Length; index++)
                            {
                                this.animationControls[index].SetActive(true);
                            }
                        }                   
                    }
                }
            }
            else if (Input.GetTouch(0). phase == TouchPhase.Moved && this.spawnedObject != null) 
            {
                this.spawnedObject.transform.position = this.m_Hits[0].pose.position;
            }

            if (this.allowMultipleObjects && Input.GetTouch(0). phase == TouchPhase.Ended)
            {
                this.spawnedObject = null;
            }
        }            
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        this.spawnedObject = Instantiate(this.spawnablePrefab, spawnPosition, Quaternion.identity);
    }
}
