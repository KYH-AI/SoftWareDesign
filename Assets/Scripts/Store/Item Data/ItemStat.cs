using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item Stat", menuName = "Scriptable Object/ItemStat")]
public class ItemStat : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Image itemImage;
    [SerializeField] private string itemIntroduction;
    [SerializeField] private int itemPrice;
    [SerializeField] private Define.ItemType itemType;

    public string ItemName { get { return itemName; } }
    public Image ItemImage { get { return itemImage; } }
    public string ItemIntroduction { get { return itemIntroduction; } }
    public int ItemPrice 
    {
        get { return itemPrice; }
        set { itemPrice = value; }
    }
    public  Define.ItemType ItemType { get { return itemType; }}
}


