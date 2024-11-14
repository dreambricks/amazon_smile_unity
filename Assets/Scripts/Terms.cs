using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terms : MonoBehaviour
{
    [SerializeField] private GameObject fitas_terms;

    public Toggle myToggle;
    public Button myButton;

    void OnEnable()
    {
        // myToggle.isOn = false;
        // UpdateButtonState(false);
        // myToggle.onValueChanged.AddListener(OnToggleValueChanged);

        myButton.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        fitas_terms.gameObject.SetActive(true);
    }

    // void OnToggleValueChanged(bool isChecked)
    // {
    //     UpdateButtonState(isChecked);
    // }

    // void UpdateButtonState(bool isActive)
    // {
    //     myButton.interactable = isActive;

    //     Image buttonImage = myButton.GetComponent<Image>();

    //     Color color = buttonImage.color;
    //     color.a = isActive ? 1f : 220f / 255f;
    //     buttonImage.color = color;
    // }


}
