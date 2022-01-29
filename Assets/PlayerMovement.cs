using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputs inputs;
    private float movementSpeed = 10.0f;
    // Start is called before the first frame update
    void Awake()
    {
        inputs = new PlayerInputs();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementInputs = inputs.Inputs.Movement.ReadValue<Vector2>();
        Debug.Log(movementInputs);
        Vector3 movement = new Vector3(movementInputs.x, 0.0f, movementInputs.y);
        movement = movement * movementSpeed * Time.deltaTime;
        Debug.Log(movement);
        this.transform.Translate(movement, Space.World);
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}
