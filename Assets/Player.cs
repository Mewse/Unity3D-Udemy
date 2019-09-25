using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 4f;
    [Tooltip("In ms^-1")][SerializeField] float ySpeed = 4f;
    [SerializeField] float xBound = 6f;
    [SerializeField] float yBound = 6f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionRollFactor = -5f;
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = -45f;

    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        float zRotation = 45 * -xThrow;
        float xRotation = 30 * -yThrow;

        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float rollDueToPosition = transform.localRotation.x * positionRollFactor;
        float rollDueToControlThrow = xThrow * controlRollFactor;

        float yaw = 0f;
        float roll = rollDueToPosition + rollDueToControlThrow;



        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        rawNewXPos = Mathf.Clamp(rawNewXPos, -xBound, xBound);

        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        rawNewYPos = Mathf.Clamp(rawNewYPos, -yBound, yBound);

        transform.localPosition = new Vector3(rawNewXPos, rawNewYPos, transform.localPosition.z);
    }
}
