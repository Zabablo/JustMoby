using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using System;

public class ViewController : MonoBehaviour
{
    public Configurations inventoryHolder;
    public Text header;
    public Text discription;
    public TMP_Text buttonPrice;
    public Transform discount;
    public Image bigIcon;
    public Transform shoppingWindow;
    public Transform startWindow;

    public Transform buildingMaterialsParent;
    private int countItems;

    private GameObject itemViewPrefab;


    private void Start()
    {
        
    }

    public void ShoppingWindow()
    {
        
        string itemCount = startWindow.GetComponent<StartWindow>().itemCount.text;
        if (int.TryParse(itemCount, out countItems) && countItems <= 6)
        {
            shoppingWindow.gameObject.SetActive(true);
            startWindow.gameObject.SetActive(false);
            InitShoppingWindow();
        }
        else
        {
            Debug.Log("¬ведите число не больше 6!");
        }
        
    }

    private void InitShoppingWindow()
    {
        itemViewPrefab = (GameObject)Resources.Load("Item");
        header.text = inventoryHolder.header;
        discription.text = inventoryHolder.discription;

        int i = 0;
        foreach (var item in inventoryHolder.inventory)
        {
            if (i == countItems)
                break;
            i++;
            var itemGO = Instantiate(itemViewPrefab, buildingMaterialsParent);
            itemGO.GetComponent<ItemView>().InitItem(item);
        }

        if (inventoryHolder.discount == 0)
        {
            buttonPrice.text = "$" + inventoryHolder.price.ToString();
        }
        else
        {
            discount.gameObject.SetActive(true);
            discount.GetComponentInChildren<Text>().text = inventoryHolder.discount.ToString() + "%";
            buttonPrice.text = "<b>$" + (inventoryHolder.price - inventoryHolder.price / 100 * inventoryHolder.discount) + "</b> \n" + "<s> $" + inventoryHolder.price.ToString() + " </s>";
        }


        bigIcon.sprite = inventoryHolder.bigIcon.icon;
    }

    public void StartWindow()
    {
        while (buildingMaterialsParent.childCount > 0)
        {
            DestroyImmediate(buildingMaterialsParent.GetChild(0).gameObject);
        }
        shoppingWindow.gameObject.SetActive(false);
        startWindow.gameObject.SetActive(true);

        
    }
}
