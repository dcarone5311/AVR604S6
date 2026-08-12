using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModeDebug : MonoBehaviour
{
    TextMeshProUGUI modeText;

    // Start is called before the first frame update
    void Start() => modeText = GetComponent<TextMeshProUGUI>();
    

    // Update is called once per frame
    void Update() => modeText.text = InteractionController.Instance.currentMode.name;
    
}
