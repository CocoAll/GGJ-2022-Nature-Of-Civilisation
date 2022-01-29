using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI header;
    public TextMeshProUGUI content;
    public LayoutElement layoutElement;

    public int characterWarpLimit;

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string header, string content = "")
    {
        if (string.IsNullOrEmpty(content))
        {
            this.content.gameObject.SetActive(false);
        }
        else
        {
            this.content.gameObject.SetActive(true);
            this.content.text = content;
        }

        this.header.text = header;

        int headerLength = this.header.text.Length;
        int contentLength = this.content.text.Length;

        layoutElement.enabled = (headerLength > characterWarpLimit || contentLength > characterWarpLimit) ? true : false;
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = this.header.text.Length;
            int contentLength = this.content.text.Length;

            layoutElement.enabled = (headerLength > characterWarpLimit || contentLength > characterWarpLimit) ? true : false;
        }

        SetPosition();
    }

    private void OnEnable()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        Vector2 position = Mouse.current.position.ReadValue();

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = position;
    }
}
