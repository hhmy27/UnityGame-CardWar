using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCardPanel : MonoBehaviour {
    public GameObject LoseCardPanel;

    public void onClick()
    {
        LoseCardPanel.SetActive(false);
    }

}
