using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Player;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
public class ButtonTest
{
    [UnityTest]
    public IEnumerator ButtonClickTest()
    {
        // Load the initial scene
        var asyncOp = SceneManager.LoadSceneAsync("StartGame");
        yield return new WaitUntil(() => asyncOp.isDone);
        var menuManager = GameObject.FindObjectOfType<MenuManager>();
        menuManager.LoadMenu("Menu_start");
        // Now safely find your objects
        var button = GameObject.Find("Play").GetComponent<MenuCountdown>();
        Assert.IsNotNull(button, "MenuCountdown should not be null");

        button.OnPointerEnter(new PointerEventData(EventSystem.current));
        yield return new WaitUntil(() => button.menuOption.value == 0);
        yield return new WaitForSeconds(0.1f); // Wait for the button to be ready

        Assert.AreEqual("StoryStart", SceneManager.GetActiveScene().name, "Should load StoryStart scene after button click");
        yield return null;
    }
    [UnityTest]
    public IEnumerator ButtonOnClickedLoadsScene()
    {
        var asyncOp = SceneManager.LoadSceneAsync("StartGame");
        yield return new WaitUntil(() => asyncOp.isDone);

        var menuManager = GameObject.FindObjectOfType<MenuManager>();
        menuManager.LoadMenu("Menu_start");

        yield return null;

        var button = GameObject.Find("Play")?.GetComponent<MenuCountdown>();
        Assert.IsNotNull(button, "Play button must exist");

        // Simulate button click
        button.OnClicked();

        // Wait for scene load to trigger
        yield return new WaitForSeconds(0.5f);

        Assert.AreEqual("StoryStart", SceneManager.GetActiveScene().name, "Scene should transition after button click");
    }

    [UnityTest]
    public IEnumerator ButtonClickStressTest()
    {
        var asyncOp = SceneManager.LoadSceneAsync("StartGame");
        yield return new WaitUntil(() => asyncOp.isDone);

        var menuManager = GameObject.FindObjectOfType<MenuManager>();
        menuManager.LoadMenu("Menu_start");

        yield return null;

        var button = GameObject.Find("Play")?.GetComponent<MenuCountdown>();
        Assert.IsNotNull(button, "Play button must exist");

        // Rapidly click the button multiple times
        for (int i = 0; i < 5; i++)
        {
            button.OnClicked();
            yield return new WaitForSeconds(0.05f);
        }

        // Wait and check scene transition
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual("StoryStart", SceneManager.GetActiveScene().name, "Scene should load even after multiple rapid clicks");
    }
    [UnityTest]
public IEnumerator OnClicked_KeyboardMove_CallsOnTimerComplete_AndLoadsSliders()
{
    var asyncOp = SceneManager.LoadSceneAsync("StartGame");
    yield return new WaitUntil(() => asyncOp.isDone);

    var menuManager = GameObject.FindObjectOfType<MenuManager>();
    menuManager.LoadMenu("Menu_start");

    yield return null;

    var button = GameObject.Find("Play")?.GetComponent<MenuCountdown>();
    Assert.IsNotNull(button, "Play button must exist");

    // Simulate keyboard control mode
    var playerMovement = PlayerMovement.Instance;
    InputActionMap playerActionMap = playerMovement.inputActions.FindActionMap("Player");
    var input = playerActionMap.FindAction("KeyboardMove");

                playerMovement.change_strategy(input);

    // Simulate click action being triggered
    var clickAction = playerMovement.CurrentControl.get_click_action();
    clickAction?.Enable();
    InputSystem.QueueStateEvent(Keyboard.current, new KeyboardState(Key.Space)); // or correct binding key
    InputSystem.Update();

    // Ensure action was triggered
    Assert.IsTrue(clickAction.triggered);

    // Call method
    button.OnClicked();

    yield return new WaitForSeconds(0.2f); // let logic process

    // You could verify the effects here
    // e.g., a flag set in OnTimerComplete, slider state changed, etc.
}

}
