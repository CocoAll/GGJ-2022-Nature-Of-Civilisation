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

    public void SetUpDisplay(Building building)
    {
        this.building = building;
        this.image = this.building.icon;
        this.name.text = building.name;
    }

    public void OnClick()
    {
        selectedBuilding.building = this.building;
        selectedBuildingSignal.Raise();
    }

}
