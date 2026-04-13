using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    public Skin[] info;
    private bool[] StockCheck;    
    
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI countmoneyText;
    public Transform player;
    public int index;

    public int money;    

    public void UpdateShop()
    {
        money = PlayerPrefs.GetInt("Money");
        moneyText.text = PlayerPrefs.GetInt("Money").ToString();
        countmoneyText.text = PlayerPrefs.GetInt("Money").ToString();
        for (int i = 0; i < info.Length; i++)
        {
            if (info[i].inStock && info[i].isChosen)
            {
                info[i].priceText.text = "CHOSEN";
                info[i].buyButton.interactable = false;
            }
            else if (!info[i].inStock)
            {
                info[i].priceText.text = info[i].cost.ToString();
                info[i].buyButton.interactable = true;
            }
            else if (info[i].inStock && !info[i].isChosen)
            {
                info[i].priceText.text = "CHOOSE";
                info[i].buyButton.interactable = true;
            }    
        }        
    }

    private void Awake()
    {
        money = PlayerPrefs.GetInt("Money");
        index = PlayerPrefs.GetInt("ChosenSkin");
        //Debug.Log("index: " + index);
        moneyText.text = money.ToString();

        StockCheck = new bool[8];
        if (PlayerPrefs.HasKey("StockArray"))
        {
            StockCheck = PlayerPrefsX.GetBoolArray("StockArray");
        }
        else
        {
            StockCheck[0] = true;
        }

        info[index].isChosen = true;

        for (int i = 0; i < info.Length; i++)
        {
            info[i].inStock = StockCheck[i];
            if (i == index)
            {
                player.GetChild(i).gameObject.SetActive(true);
                info[i].isChosen = true;
            }
            else
            {
                player.GetChild(i).gameObject.SetActive(false);
                info[i].isChosen = false;
            }
        }
    }    
    
    public void Save()
    {        
        PlayerPrefsX.SetBoolArray("StockArray", StockCheck);
    }

    public void BuyButtonAction(int indx)
    {
        index = indx;
        if (info[index].buyButton.interactable && !info[index].inStock)
        {
            if (money >= int.Parse(info[index].priceText.text))
            {
                money -= int.Parse(info[index].priceText.text);
                moneyText.text = money.ToString();
                PlayerPrefs.SetInt("Money", money);
                StockCheck[index] = true;
                info[index].inStock = true;
                info[index].priceText.text = "CHOOSE";
                Save();
            }
        }

        if (info[index].buyButton.interactable && !info[index].isChosen && info[index].inStock)
        {
            for (int i = 0; i < info.Length; i++)
            {
                info[i].isChosen = false;
                player.GetChild(i).gameObject.SetActive(false);
            }
            player.GetChild(index).gameObject.SetActive(true);
            PlayerPrefs.SetInt("ChosenSkin", index);
            info[index].isChosen = true;
            info[index].buyButton.interactable = false;
            info[index].priceText.text = "CHOSEN";           
        }
        UpdateShop();        
    }

    public void AddftyMoney()
    {
        money += 50;
        moneyText.text = money.ToString();
        PlayerPrefs.GetInt("Money", money);       
    }
}

[System.Serializable]
public class Skin
{
    public Button buyButton;
    public TextMeshProUGUI priceText;    
    public int cost;
    public bool inStock;
    public bool isChosen;
}
