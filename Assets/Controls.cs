using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class Controls : MonoBehaviour
{
    public Camera SceneCamera;
    private int currentMode = 1;

    public Arduino arduino;

    public int button1Pin = 3;
    public int button2Pin = 5;

    private bool button1On = false;
    private bool button2On = false;

    // Use this for initialization
	void Start ()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((arduino.digitalRead(button1Pin) == Arduino.HIGH || Input.GetKey("left")) && !button1On)
        {
            Debug.Log("BUTTON 1 ON");
            button1On = true;
            Previous();
        }
        else if ((arduino.digitalRead(button1Pin) == Arduino.LOW && !Input.GetKey("left")) && button1On)
        {
            Debug.Log("BUTTON 1 OFF");
            button1On = false;
        }

        if ((arduino.digitalRead(button2Pin) == Arduino.HIGH || Input.GetKey("right")) && !button2On)
        {
            Debug.Log("BUTTON 2 ON");
            button2On = true;
            Next();
        }
        else if ((arduino.digitalRead(button2Pin) == Arduino.LOW && !Input.GetKey("right")) && button2On)
        {
            Debug.Log("BUTTON 2 OFF");
            button2On = false;
        }
	}

    public void ConfigurePins()
    {
        arduino.pinMode(button1Pin, PinMode.INPUT);
        arduino.pinMode(button2Pin, PinMode.INPUT);

        arduino.reportDigital((byte)(button1Pin / 8), 1);
        arduino.reportDigital((byte)(button2Pin / 8), 1);

        arduino.pinMode(13, PinMode.OUTPUT);
        arduino.digitalWrite(13, Arduino.HIGH); // led ON
    }

    public void Next()
    {
        currentMode++;
        if (currentMode > 5)
        {
            currentMode = 1;
        }
        SetMode(currentMode);
    }

    public void Previous()
    {
        currentMode--;
        if (currentMode < 1)
        {
            currentMode = 5;
        }
        SetMode(currentMode);
    }

    private void SetMode(int mode)
    {
        switch (mode)
        {
            case 1:
                SceneCamera.cullingMask = (1 << LayerMask.NameToLayer("Default")) |
                    (1 << LayerMask.NameToLayer("SearchTeamOverviews")) |
                    (1 << LayerMask.NameToLayer("InfoIcons")) |
                    (1 << LayerMask.NameToLayer("Ground&Water"));

                break;
            
            case 2:
                SceneCamera.cullingMask = (1 << LayerMask.NameToLayer("Default")) |
                    (1 << LayerMask.NameToLayer("SearchTeamOverviews")) |
                    (1 << LayerMask.NameToLayer("SearchTeamDetails")) |
                    (1 << LayerMask.NameToLayer("InfoIcons")) |
                    (1 << LayerMask.NameToLayer("Ground&Water"));

                break;
            
            case 3:
                SceneCamera.cullingMask = (1 << LayerMask.NameToLayer("Default")) |
                    (1 << LayerMask.NameToLayer("SearchTeamOverviews")) |
                    (1 << LayerMask.NameToLayer("InfoIcons")) |
                    (1 << LayerMask.NameToLayer("Wind")) |
                    (1 << LayerMask.NameToLayer("Ground&Water"));

                break;

            case 4:
                SceneCamera.cullingMask = (1 << LayerMask.NameToLayer("Default")) |
                    (1 << LayerMask.NameToLayer("SearchTeamOverviews")) |
                    (1 << LayerMask.NameToLayer("InfoIcons")) |
                    (1 << LayerMask.NameToLayer("Clouds")) |
                    (1 << LayerMask.NameToLayer("Wind")) |
                    (1 << LayerMask.NameToLayer("Ground&Water"));

                break;

            case 5:
                SceneCamera.cullingMask = (1 << LayerMask.NameToLayer("Default")) |
                    (1 << LayerMask.NameToLayer("SearchTeamOverviews")) |
                    (1 << LayerMask.NameToLayer("SearchTeamDetails")) |
                    (1 << LayerMask.NameToLayer("InfoIcons"));

                break;
            
            default:
                break;
        }
    }
}
