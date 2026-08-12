using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectOnPlane : MonoBehaviour
{

    [SerializeField] GameObject placedPrefab;
    GameObject spawnedObject;
    ARRaycastManager raycaster;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        raycaster = GetComponent<ARRaycastManager>();
    }

    void OnPlaceObject (InputValue value)
    {
        //get the screen touch location
        Vector2 touchPos = value.Get<Vector2>();
        
        if (raycaster.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon)) //if trackedplane is tapped
        {
            Pose hitPose = hits[0].pose;

            if(spawnedObject == null) //havent instantiated yet
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);


            }
            else
            {
                spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }

        }

    }
}
