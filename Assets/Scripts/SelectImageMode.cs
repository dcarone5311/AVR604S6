using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectImageMode : MonoBehaviour
{

    [SerializeField] AddPictureMode addPicture;
    [SerializeField] EditPictureMode editPicture;
    public bool isReplacing = false;

    private void OnEnable()
    {
        UIController.ShowUI("SelectImage");
    }

    public void ImageSelected(ImageInfo image)
    {
        addPicture.imageInfo = image;
        InteractionController.EnableMode("AddPicture");

        if(isReplacing)
        {
            editPicture.currentPicture.SetImage(image);
            InteractionController.EnableMode("EditPicture");

        }
        else
        {
            addPicture.imageInfo = image;
            InteractionController.EnableMode("AddPicture");

        }

    }

}
