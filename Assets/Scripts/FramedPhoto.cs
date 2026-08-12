using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.PropertyVariants.TrackedProperties;

public class FramedPhoto : MonoBehaviour
{
    [SerializeField] Transform scalerObject;
    [SerializeField] GameObject imageObject;
    [SerializeField] Collider boundingCollider;
    int layer;

    ImageInfo imageInfo;
    [SerializeField] GameObject highlightObject;
    bool isEditing;

    MovePicture movePicture;
    ResizePicture resizePicture;

    private void Awake()
    {
        layer = LayerMask.NameToLayer("PlacedObjects");
        Highlight(false);

        movePicture = GetComponent<MovePicture>();
        resizePicture = GetComponent<ResizePicture>();
        movePicture.enabled = true;
        resizePicture.enabled = true;


    }

    public void SetImage(ImageInfo image)
    {
        imageInfo = image;

        Renderer renderer = imageObject.GetComponent<Renderer>();
        Material material = renderer.material;
        material.SetTexture("_MainTex", imageInfo.texture);
        AdjustScale();

      
    }

    public void AdjustScale()
    {

        Vector2 scale = ImagesData.AspectRatio(imageInfo.width, imageInfo.height);
        scalerObject.localScale = new Vector3(scale.x, scale.y, 1f);

    }


    public void BeingEdited(bool editing)
    {
        Highlight(editing);
        isEditing = editing;

        movePicture.enabled = editing;
        resizePicture.enabled = editing;

    }

    public void Highlight(bool show)
    {
        if(highlightObject)
            highlightObject.SetActive(show);

    }

    private void OnTriggerStay(Collider other)
    {
        const float spacing = 0.1f;
        if ( isEditing && other.gameObject.layer == layer)
        {
            Bounds bounds = boundingCollider.bounds;
            {
                Vector3 centerDistance = bounds.center - other.bounds.center;
                Vector3 distOnPlane = Vector3.ProjectOnPlane(centerDistance, transform.forward);
                Vector3 direction = distOnPlane.normalized;
                float distanceToMoveThisFrame = bounds.size.x * spacing;
                transform.Translate ( direction * distanceToMoveThisFrame );
            }
        }
    }

}
