using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class BullyModeActivation : MonoBehaviour
{
    private bool bullyActivation = false;

    public TMP_Text bullyText;
    // Start is called before the first frame update
    void Start()
    {
        //bullyText = GetComponent<TextMesh>();
        bullyText.text = "Bully Mode: Deactivated";
        bullyText.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            bullyActivation = !bullyActivation;

            if (bullyActivation)
            {
                bullyText.text = "Bully Mode: Activated";
                bullyText.color = Color.green;
            }

            else
            {
                bullyText.text = "Bully Mode: Deactivated";
                bullyText.color = Color.red;
            }
        }
    }
}
