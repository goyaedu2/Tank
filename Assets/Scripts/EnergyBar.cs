using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    Transform mainCamera;

    [SerializeField] Image bar;

    RectTransform rectTransform;

    public void SetValue(float value)
    {
        rectTransform.localScale = new Vector3(value, 1, 1);
    }

    private void Start()
    {
        mainCamera = Camera.main.transform;
        rectTransform = bar.GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.rotation * Vector3.forward,
            mainCamera.rotation * Vector3.up);
    }
}
