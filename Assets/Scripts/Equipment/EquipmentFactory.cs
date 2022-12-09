using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EquipmentFactory : MonoBehaviour {
    public static EquipmentFactory instance;

    public GameObject[] weapons;
    public GameObject[] subWeapons;
    public GameObject[] mods;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    private void Start() {
        Assert.IsNotNull(weapons);
        weapons = Resources.LoadAll<GameObject>("Weapons");
        subWeapons = Resources.LoadAll<GameObject>("SubWeapons");
        // mods = Resources.LoadAll<GameObject>("Mods");
    }

    public IEquipment CreateRandomEquipment(EquipmentType type, int upgradeRanks, Vector2 position) {
        GameObject item;
        if(type == EquipmentType.WEAPON) {
            item = Instantiate(weapons[Random.Range(0, weapons.Length)], position, Quaternion.identity);
        } else {
            item = Instantiate(subWeapons[Random.Range(0, subWeapons.Length)], position, Quaternion.identity);
        }

        IEquipment newEquip = item.GetComponent<IEquipment>();
        Assert.IsNotNull(newEquip);
        
        newEquip.Upgrade(upgradeRanks);

        return newEquip;
    }

    public Color GetRarityColor(Rarity rarity) {
        var color = rarity switch
        {
            Rarity.COMMON => Color.yellow,
            Rarity.UNCOMMON => Color.green,
            Rarity.RARE => Color.blue,
            Rarity.LEGENDARY => Color.magenta,
            _ => Color.yellow,
        };
        color.a = .6f;
        return color;
    }
}
