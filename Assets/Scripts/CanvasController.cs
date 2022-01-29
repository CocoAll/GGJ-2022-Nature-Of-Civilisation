using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private List<Resource> resources;
    [SerializeField]
    private GameObject resourceUI;
    [SerializeField]
    private GameObject resourcePanel;

    private List<ResourceUIDisplay> allResourceUI = new List<ResourceUIDisplay>();

    private void Awake()
    {
        if(resources == null || resources.Count == 0)
        {
            throw new System.Exception("Le canvas controller n'a pas de resource à afficher!");
        }

        foreach(Resource rsc in resources)
        {
            ResourceUIDisplay resourceElement = Instantiate(resourceUI, resourcePanel.transform).GetComponent<ResourceUIDisplay>();
            resourceElement.SetUpUI(rsc);
            allResourceUI.Add(resourceElement);
        }
    }

    public void OnResourceUpdated()
    {
        foreach(ResourceUIDisplay resource in allResourceUI)
        {
            resource.UpdateUI();
        }
    }
}
