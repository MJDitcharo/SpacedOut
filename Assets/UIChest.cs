using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChest : PopUpMenu
{
    [SerializeField]
    private GameObject slotParent;
    [SerializeField]
    public GameObject chestVisual;

    private List<TMPro.TextMeshProUGUI> childrenText = new List<TMPro.TextMeshProUGUI>();
    private void Start()
    {
        int i = 0;
        //grab all text components at the start
        foreach (Transform t in slotParent.transform)
            childrenText.Add(t.gameObject.GetComponent<TMPro.TextMeshProUGUI>());
    }

    public void Activate(List<Pickups> chestContents)
    {
        for (int i = 0; i < chestContents.Count; i++)
            childrenText[i].text = chestContents[i].GetString();

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
}
