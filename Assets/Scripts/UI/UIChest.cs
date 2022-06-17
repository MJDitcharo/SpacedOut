using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChest : PopUpMenu
{
    [SerializeField]
    private GameObject slotParent;
    [SerializeField]
    public GameObject chestVisual;

    static int slotCount = 0;

    private List<TMPro.TextMeshProUGUI> childrenText = new();
    private void Start()
    {
        //grab all text components at the start
        int i = 0;
        foreach (Transform t in slotParent.transform)
            childrenText.Add(t.gameObject.GetComponent<TMPro.TextMeshProUGUI>());
    }

    public void Activate()
    {
        GameManager.instance.SetNormalCursor();
        chestVisual.SetActive(true);
        FreezeWorld();
    }

    public void Deactivate()
    {
        GameManager.instance.SetFightingCursor();
        chestVisual.SetActive(false);
        foreach (TMPro.TextMeshProUGUI text in childrenText)
            text.text = string.Empty;
        slotCount = 0;
        UnfreezeWorld();
    }

    public bool VisualIsActive()
    {
        return chestVisual.activeInHierarchy;
    }

    public int GetChildCount()
    {
        return slotParent.transform.childCount;
    }
    //public void SetText(string name, string value)
    //{
    //    slotParent.transform.Find(name).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = value;
    //}

    public void SetText(string value)
    {
        childrenText[slotCount].text = value;
        slotCount++;
    }
    override public bool IsActive()
    {
        return chestVisual.activeSelf;
    }
}
