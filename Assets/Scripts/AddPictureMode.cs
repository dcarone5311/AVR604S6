using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AddPictureMode : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycaster;
    [SerializeField] GameObject placePrefab;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public ImageInfo imageInfo;
    [SerializeField] float defaultScale = 0.5f;


    private void OnEnable()
    {
        UIController.ShowUI("AddPicture");
    }
    
    public void OnPlaceObject(InputValue value)
    {
        Vector2 touchPosition = value.Get<Vector2>();
        PlaceObject(touchPosition);
    }

    void PlaceObject(Vector2 touchPosition)
    {
        if (raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            //if a tap on a plane is detected

            Pose hitPose = hits[0].pose;

            Vector3 position = hitPose.position;
            Vector3 normal = -hitPose.up;
            Quaternion rotation = Quaternion.LookRotation(normal, Vector3.up);

            GameObject spawned = Instantiate(placePrefab, position, rotation);
            spawned.transform.SetParent(transform.parent);

            FramedPhoto picture = spawned.GetComponent<FramedPhoto>();
            picture.SetImage(imageInfo);
            spawned.transform.localScale = new Vector3(defaultScale, defaultScale, 1.0f);


            InteractionController.EnableMode("Main"); // go back to main

        }


    }
}
