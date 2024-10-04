using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CardMatch_Gameplay;

[System.Serializable]
public class SaveObject
{
    public int cardNumber = 0;
    public int spriteIndex = 0;
    public CardState cardState = CardState.CLOSE;
    public bool isOn = false;

}
[System.Serializable]
public class SaveObjectInventory
{
   public List<SaveObject> saveObjects = new List<SaveObject>();
    public int grideSize = 0;
    public bool hasValue = false;

}
public class InventoryManager : MonoBehaviour
{
    public SaveObjectInventory inventory = null;


    public void updateList()
    {
        try
        {
            CardClickListiner[] cards = GameManager.Instance.cardManager.cards;
            inventory.saveObjects.Clear();
            if (cards != null && cards.Length > 0)
            {
                int index = 0;
                foreach (var item in cards)
                {
                    if (item != null)
                    {
                        SaveObject temp = new SaveObject();
                        temp.cardNumber =int.Parse(item.gameObject.name)-1;
                        if (item.CardSprite != null)
                        {
                            temp.spriteIndex = int.Parse(item.CardSprite.name);
                        }
                        else {
                            temp.spriteIndex = 0;
                        }
                        temp.cardState = item.cardState;
                        temp.isOn = item.gameObject.activeSelf;
                        inventory.saveObjects.Add(temp);
                    }
                    index = index + 1;
                }
                inventory.hasValue = true;
                inventory.grideSize = GameManager.Instance.currentGridSize;
                string jsonString = JsonUtility.ToJson(inventory);
                PlayerPrefs.SetString("INVNETORY", jsonString);
                PlayerPrefs.Save();
            }
        }
        catch (Exception e)
        {
            clearData();
        }

    }


    public bool hasPrefValue() {
        try
        {
            string oldInventory = PlayerPrefs.GetString("INVNETORY", "");
            SaveObjectInventory prefInventory = JsonUtility.FromJson<SaveObjectInventory>(oldInventory);
            if (prefInventory == null || prefInventory.saveObjects == null)
            {
                
                return false;
            }
            return true;


        }
        catch (Exception e)
        {

            return false;
        }
    }

    public void setupPrefInventory()
    {
        try
        {
            string oldInventory = PlayerPrefs.GetString("INVNETORY", "");
            SaveObjectInventory prefInventory = JsonUtility.FromJson<SaveObjectInventory>(oldInventory);
            if (prefInventory == null || prefInventory.saveObjects == null)
            {

                return;
            }
            inventory = prefInventory;

            if (inventory.hasValue)
            {
                GameManager.Instance.cardManager.enableCard(inventory.grideSize);
                CardClickListiner[] cards = GameManager.Instance.cardManager.cards;

                foreach (var item in inventory.saveObjects)
                {
                   
                    cards[item.cardNumber].CardSprite = GameManager.Instance.cardManager.cardSprites[item.spriteIndex];
                    cards[item.cardNumber].resetCard();

                    cards[item.cardNumber].gameObject.SetActive(item.isOn);
                }
            }
            clearData();
         GameManager.Instance.doChangeState(GAMESTATES.SHOWALL);


        }
        catch (Exception e)
        {

            clearData();
        }


    }



#if UNITY_EDITOR
     private void OnApplicationQuit()
    {
        updateList();
    }
#elif UNITY_IOS
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            updateList();
        }
    }

#elif UNITY_ANDROID
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            updateList();
        }
    }
#endif
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        setupPrefInventory();
    }

    public void clearData()
    {

        PlayerPrefs.SetString("INVNETORY", "");
        PlayerPrefs.Save();
    }


}
