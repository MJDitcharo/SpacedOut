using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChest : PopUpMenu
{
    [SerializeField]
    private GameObject slotParent;
    [SerializeField]
    public GameObject chestVisual;

    private List<TMPro.TextMeshProUGUI> childrenText = new();
    private void Start()
    {
        //grab all text components at the start
        foreach (Transform t in slotParent.transform)
            childrenText.Add(t.gameObject.GetComponent<TMPro.TextMeshProUGUI>());
    }

    public void Activate()
    {
        chestVisual.SetActive(true);
        FreezeWorld();
    }

    public void Deactivate()
    {
        chestVisual.SetActive(false);
        foreach (TMPro.TextMeshProUGUI text in childrenText)
            text.text = string.Empty;
        UnfreezeWorld();
    }

    public int GetChildCount()
    {
        return slotParent.transform.childCount;
    }
    public void SetText(string name, string value)
    {
        slotParent.transform.Find(name).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = value;
    }
    override public bool IsActive()
    {
        return chestVisual.activeSelf;
    }
}
