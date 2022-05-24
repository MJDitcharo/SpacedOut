using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : PopUpMenu
{
    public static UIShop instance;
    public int skrap = 300;
    public Upgrade[] upgrades;

    //Ref
    public Text cointText;
    public GameObject shopUI;
    public Transform shopContent;
    public GameObject itemPrefab;
    public PlayerMovement player;

    private Transform container;
    private Transform shopItemTemplate;
    private void Awake()
    {
        if (instance == null)
        { instance = this; } 
        else {
            Destroy(gameObject); 
        }
        DontDestroyOnLoad(gameObject);
    }
   
    private void Start()
    {
        TMPro.TextMeshProUGUI text = transform.Find("Skrap").GetComponent<TMPro.TextMeshProUGUI>();
        //GameMananger.instance.boardWipeCost = text.text
    }




    //private void Start()
    //{
        //foreach (Upgrade upgrade in upgrades)
        //{
        //    GameObject item = Instantiate(itemPrefab, shopContent);
        //    upgrade.itemRef = item;
        //    foreach (Transform child in item.transform)
        //    {
        //        if (child.gameObject.name == "Quantity")
        //        {
        //            child.gameObject.GetComponent<Text>().text = upgrade.quantity.ToString();
        //        } else if (child.gameObject.name == "Cost")
        //        {
        //            child.gameObject.GetComponent<Text>().text = "$" + upgrade.cost.ToString();
        //        }
        //        else if (child.gameObject.name == "Name")
        //        {
        //            child.gameObject.GetComponent<Text>().text = upgrade.name;
        //        }
        //        else if (child.gameObject.name == "Image")
        //        {
        //            child.gameObject.GetComponent<Image>().sprite = upgrade.image;
        //        }
        //    }
        //    item.GetComponent<Button>().onClick.AddListener(() =>
        //    {
        //        BuyUpgrade(upgrade);
        //    });
        //}
   // }
    public void BuyUpgrade (Upgrade upgrade)
    {
        //if (coins >= upgrade.cost)
        //{
        //    coins -= upgrade.cost;
        //    upgrade.quantity++;
        //    upgrade.itemRef.transform.GetChild(0).GetComponent<Text>().text = upgrade.quantity.ToString();
        //    ApplyUpgrade(upgrade);
        //}
    }
    public void ApplyUpgrade(Upgrade upgrade)
    {

    }
    public void ToggleShop()
    {
        shopUI.SetActive(!shopUI.activeSelf);
    }
    private void OnGui()
    {
        //cointText.text = "Skrap: " + coins.ToString();
    }

    public static string Item01;
    public static string Item02;
    public static string Item03;
    public static string Item04;
    public static string Item05;
    public static int ShopItem;

    public void Update()
    {

    }

    //private void Awake()
    //{
    //    container = transform.Find("container");
    //    shopItemTemplate = container.Find("shopItemTemplate");
    //    shopItemTemplate.gameObject.SetActive(false);
    //}
    //private void Start()
    //{
    //    //FreezeWorld();

    //}
    //private void CreateItemButton(Sprite itemSprite, string itemName, int itemCost) 
    //{
    //    Transform shopItemTransform = Instantiate(shopItemTemplate, container);
    //    RectTransform shopItemRectTransform.shopItemTransform.GetComponent<RectTransform>();
    //    float shopItemHeight = 30f;
    //    shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
    //    shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
    //    shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
    //    shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
    //}
    //public void HealthUpgrade()
    //{
    //    if (GameManager.instance.skrapCount >= 200)
    //    {
    //        if (GameManager.instance.playerScript.GetCurrentPlayerHealth() != GameManager.instance.playerScript.GetPlayersMaxHealth())
    //        {
    //            GameManager.instance.skrapCount -= 200;
    //            UpdateCashCountShopUi();
    //            GameManager.instance.playerHealth.IncreasePlayerHealth(GameManager.Player.GetPlayersMaxHealth());
    //            GameManager.instance.healthBarScript.UpdateHealthBar();
    //        }
    //    }

    //}

}

[System.Serializable]
public class Upgrade
{
    public string name;
    public int cost;
    public Sprite image;
    [HideInInspector] public int quantity;
    [HideInInspector] public GameObject itemRef;
}