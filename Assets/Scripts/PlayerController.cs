using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Camera camera;

    private Tile tileToConstruct;

    PlayerInputs inputs;
    Mouse mouse;

    private void Awake()
    {
        inputs = new PlayerInputs();

        inputs.Inputs.Action.performed += _ => OnMousePressed();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMousePressed()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(Physics.Raycast(ray, out hit))
        {
            GameObject go = hit.transform.gameObject;
            Debug.Log(go.name + " : " + go.transform.position);
        }
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
