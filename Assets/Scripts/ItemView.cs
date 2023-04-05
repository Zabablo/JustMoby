using UnityEngine;
using UnityEngine.UI;


public class ItemView : MonoBehaviour
{
    public Image itemIcon;
    public void InitItem(ItemData item)
    {
        itemIcon.sprite = item.icon;
    }
}