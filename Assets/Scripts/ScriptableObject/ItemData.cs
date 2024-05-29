using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}

public enum ConsumableType
{
    Hunger,
    Health,
    Speed
}

public enum EquipBuffType
{
    Speed,
    Jump,
    Health
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

    [System.Serializable]
    public class ItemDataEquip
    {
        public EquipBuffType equipBuffType;
        public float value;
    }


    [CreateAssetMenu(fileName = "Item", menuName = "New Item")]
    public class ItemData : ScriptableObject
    {
        [Header("Info")]
        public string displayName;
        public string description;
        public ItemType type;
        public Sprite icon;
        public GameObject dropPrefab;

        [Header("Stacking")]
        public bool canStack;
        public int maxStackAmount;

        [Header("Consumable")]
        public ItemDataConsumable[] consumables;

        [Header("Equip")]
        public ItemDataEquip[] EquipItems;
        public GameObject equipPrefab;
    }
