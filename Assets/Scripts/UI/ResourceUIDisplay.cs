using UnityEngine;
using UnityEngine.UI;

public class ResourceUIDisplay : MonoBehaviour
{
    public Resource resource;
    [SerializeField]
    private TMPro.TextMeshProUGUI quantityText;
    [SerializeField]
    private Image resourceIcon;
    [SerializeField]
    private TooltipTrigger tooltip;

    public void SetUpUI(Resource resource)
    {
        this.resource = resource;
        this.quantityText.text = "0";
        this.resourceIcon.sprite = this.resource.icon;

        tooltip.header = this.resource.name;
        tooltip.content = "";
    }

    public void UpdateUI()
    {
        this.quantityText.text = string.Format("{0}", resource.quantity);
    }
}
