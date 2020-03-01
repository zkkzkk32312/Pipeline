using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InputFieldKeyPressed : MonoBehaviour
{
    public Button bt;
    TMP_InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputField.text != string.Empty && Input.GetKeyUp(KeyCode.Return))
        {
            bt.onClick.Invoke();

            //EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
            //inputField.OnPointerClick(new PointerEventData(EventSystem.current));

            inputField.ActivateInputField();
        }
    }
}
