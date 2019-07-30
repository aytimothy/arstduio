using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class CreateObjectSearchTextbox : MonoBehaviour {
    public Button clearButton;
    public TMP_InputField textBox;
    bool warningShown = false;

    void Start() {
        if (clearButton.onClick.GetPersistentEventCount() == 0)
            clearButton.onClick.AddListener(ClearButton_OnClick);
        if (textBox == null) textBox = GetComponent<TMP_InputField>();
        if (textBox.onValueChanged.GetPersistentEventCount() == 0)
            textBox.onValueChanged.AddListener(TextBox_OnEdit);
        TextBox_OnEdit(textBox.text);
    }

    public void TextBox_OnEdit(string newText) {
        clearButton.gameObject.SetActive(!string.IsNullOrEmpty(newText));
        if (!warningShown) {
            warningShown = true;
            Debug.LogWarning("Not Implemented: *Actually* Search and hide stuff in the list of objects.");
        }
    }

    public void ClearButton_OnClick() {
        textBox.text = "";
    }
}
