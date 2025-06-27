using System.Collections;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Player;
using Inventory;
using Weapons;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerTestSimplePasses()
    {

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerTestWithEnumeratorPasses()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/StartGame.unity");
        var playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        Assert.IsNotNull(playerMovement, "PlayerMovement should not be null");
        var player = GameObject.FindObjectOfType<PlayerManager>();
        Assert.IsNull(player, "PlayerManager should be null at the start");
        var asyncOp = EditorSceneManager.OpenScene("Assets/Scenes/Gameplay.unity");
<<<<<<< HEAD
        yield return null;
=======
        yield return null; // Wait a frame for the scene to load
>>>>>>> origin/fight_Scene
        player = GameObject.FindObjectOfType<PlayerManager>();
        Assert.IsNotNull(player, "PlayerManager should be initialized after loading Gameplay scene");
        yield return null;
    }

}
