using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUIDisplay : MonoBehaviour
{
    private Building building;
    [SerializeField]
    private BuildingReference selectedBuilding;
    [SerializeField]
    private SignalSender selectedBuildingSignal;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMPro.TextMeshProUGUI name;

    [SerializeField]
    private TooltipTrigger tooltip;

    public void SetUpDisplay(Building building)
    {
        this.building = building;
        this.image.sprite = this.building.icon;
        this.name.text = building.name;

        this.tooltip.header = building.name;

        string content = "";
        foreach(ResourceCostStruct resourceStruct in building.resourceCosts)
        {
            content += resourceStruct.resource.name + " : " + resourceStruct.Count + "\n";
        }
        this.tooltip.content = content;
    }

    public void OnClick()
    {
        selectedBuilding.building = this.building;
        selectedBuildingSignal.Raise();
    }

}
