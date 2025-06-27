using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;
using Inventory;
using Food;
using Weapons;
using UnityEditor;
using Player;

public class InventoryTests
{
    [Test]
    public void InventoryFirstTest()
    {
        //setare scena de testare
        var asyncOp = EditorSceneManager.OpenScene("Assets/Scenes/Gameplay.unity");
        var player = GameObject.FindObjectOfType<PlayerManager>();
        var inventory = GameObject.FindObjectOfType<InventoryManager>();

        //verificare inventar este gol la inceput
        Assert.IsEmpty(inventory.Weapons, "Weapons should be empty at the start");
        Assert.IsEmpty(inventory.Food, "Food should be empty at the start");
        Assert.IsEmpty(inventory.Items, "Items should be empty at the start");
        Assert.IsEmpty(inventory.Spells, "Spells should be empty at the start");
        Assert.IsEmpty(inventory.Artefacts, "Artefacts should be empty at the start");

        //verificare adaugare arma inventar
        var weapon = new WeaponB("TestWeapon", "Test Description", false, true, 10f, null);
        inventory.Weapons.Add(weapon);
        Assert.Contains(weapon, inventory.Weapons, "Weapon should be added to the inventory");

        //verificare consumare mancare din inventar
        var food = ScriptableObject.CreateInstance<FoodBase>();
        food.name = "TestFood";
        food.Hp = 10;
        food.Img = null;
        if (food == null)
        {
            inventory.Food.Add(food);
            Assert.Contains(food, inventory.Food, "Food should be added to the inventory");
            inventory.Consume(food);
            Assert.IsEmpty(inventory.Food, "Food should be consumed and removed from the inventory");
        }
    }
        
}
