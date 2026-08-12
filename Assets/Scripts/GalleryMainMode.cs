using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GalleryMainMode : MonoBehaviour
{
    [SerializeField] EditPictureMode editMode;
    [SerializeField] SelectImageMode selectImage;
    public Button[] buttons;
    Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }



    private void OnEnable()
    {
        UIController.ShowUI("Main");

        Invoke("EnableButtons", 1f);

    }


    void EnableButtons()
    {
        foreach (var button in buttons)
        {
            button.interactable = true;

        }


    }

    public void OnSelectObject (InputValue value)
    {
        Vector2 touchPosition = value.Get<Vector2>();
        FindObjectToEdit(touchPosition);

    }


    void FindObjectToEdit(Vector2 touchPosition)
    {
        Ray ray = camera.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("PlacedObjects");

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            FramedPhoto picture = hit.collider.GetComponentInParent<FramedPhoto>();
            editMode.currentPicture = picture;
            InteractionController.EnableMode("EditPicture");

        }


    }    

    public void SelectImageToAdd()
    {
        selectImage.isReplacing = false;
        InteractionController.EnableMode("SelectImage");

    }

}
