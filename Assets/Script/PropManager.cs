using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    public int coinCount;// �������
    public int equipmentCnt;// װ������
    public int keyCnt;// Կ������
    public int potionCnt;// ҩ������


    public bool PickupItem(GameObject obj)
    {
        switch (obj.tag)
        {
            case Constants.Tag_COIN:
                PickUpCoin();
                return true;
            case Constants.Tag_EQUIPMENT:
                PickUpEquipment();
                return true;
            case Constants.Tag_KEY:
                PickUpKey();
                return true;
            case Constants.Tag_POTION:
                PickupPotion();
                return true;
            default:
                Debug.Log("�޷�ʰȡ");
                return false;

        }
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = 50;
        GUI.Label(new Rect(20,20,500,500),"Coin Nums: " + coinCount);
        GUI.Label(new Rect(20, 100, 500, 500),"Equipment Nums:"+equipmentCnt);
        GUI.Label(new Rect(20, 180, 500, 500),"Key Nums:" + keyCnt);
        GUI.Label(new Rect(20, 260, 500, 500),"Potion Num:" + potionCnt);
    }

    private void PickUpCoin()
    {
        coinCount++;
    }

    private void PickUpEquipment()
    {
        equipmentCnt++;
    }

    private void PickUpKey()
    {
        keyCnt++;
    }
    
    private void PickupPotion()
    {
        potionCnt++;
    }    
}
