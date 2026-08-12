using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditPictureMode : MonoBehaviour
{
    public FramedPhoto currentPicture;
    [SerializeField] SelectImageMode selectImage;

    Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }
    private void OnEnable()
    {
        UIController.ShowUI("EditPicture");

        if (currentPicture)
            currentPicture.BeingEdited(true);

    }

    private void OnDisable()
    {
        if (currentPicture)
            currentPicture.BeingEdited(false);
    }


    public void OnSelectObject(InputValue value)
    {
        Vector2 touchPosition = value.Get<Vector2>();
        FindObjectToEdit(touchPosition);

    }


    void FindObjectToEdit(Vector2 touchPosition)
    {
        Ray ray = camera.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("PlacedObjects");

        if (Physics.Raycast(ray, out hit, 50f, layerMask))
        {
            FramedPhoto picture = hit.collider.GetComponentInParent<FramedPhoto>();
            currentPicture = picture;
            InteractionController.EnableMode("EditPicture");

        }
    }

    public void DeletePicture()
    {
        Destroy(currentPicture.gameObject);
        InteractionController.EnableMode("Main");

    }

    public void SelectImageToReplace()
    {
        selectImage.isReplacing = true;
        InteractionController.EnableMode("SelectImage");
    }

}
