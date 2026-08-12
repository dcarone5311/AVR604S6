using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;

public class ShowTrackablesOnEnable : MonoBehaviour
{
    [SerializeField] XROrigin sessionOrigin;
    ARPlaneManager planeManager;
    ARPointCloudManager cloudManager;
    bool isStarted;


    // Start is called before the first frame update
    void Awake()
    {
        planeManager = sessionOrigin.GetComponent<ARPlaneManager>();
        cloudManager = sessionOrigin.GetComponent<ARPointCloudManager>();
    }

    private void Start()
    {
        isStarted = true;
    }


    private void OnEnable()
    {
        ShowTrackables(true);
    }

    private void OnDisable()
    {
        if (isStarted)
        {
            ShowTrackables(false);
        }

    }

    void ShowTrackables(bool show)
    {
        if(cloudManager)
        {
            cloudManager.SetTrackablesActive(show);
            cloudManager.enabled = show;

        }

        if(planeManager)
        {
            planeManager.SetTrackablesActive(show);
            planeManager.enabled = show;
        }

    }
}
