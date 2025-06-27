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
        //incarcare scena StartGame
        var asyncOp = SceneManager.LoadSceneAsync("StartGame");
        //se asteapta ca scena sa fie incarcata complet
        yield return new WaitUntil(() => asyncOp.isDone);
        var menuManager = GameObject.FindObjectOfType<MenuManager>();
        menuManager.LoadMenu("Menu_start");
        
        //cautare obiecte din joc ce reprezinta butonul Play 
        var btn = GameObject.Find("Play").GetComponent<MenuCountdown>();
        Assert.IsNotNull(btn, "MenuCountdown should not be null");

        btn.OnPointerEnter(new PointerEventData(EventSystem.current));
        yield return new WaitUntil(() => btn.menuOption.value == 0);
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

        var btn = GameObject.Find("Play")?.GetComponent<MenuCountdown>();
        Assert.IsNotNull(btn, "Play button must exist");

        btn.OnClicked();

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

        var btn = GameObject.Find("Play")?.GetComponent<MenuCountdown>();
        Assert.IsNotNull(btn, "Play button must exist");

        var i = 0;
        while (i != 10)
        {
            btn.OnClicked();
            yield return new WaitForSeconds(0.05f);
            i++;
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

        var btn = GameObject.Find("Play")?.GetComponent<MenuCountdown>();
        Assert.IsNotNull(btn, "Play button must exist");

        // simulare control tastatura 
        var playerMovement = PlayerMovement.Instance;
        InputActionMap playerActionMap = playerMovement.inputActions.FindActionMap("Player");
        var input = playerActionMap.FindAction("KeyboardMove");

        playerMovement.change_strategy(input);

        // simulare buton enter apasat
        var clickAction = playerMovement.CurrentControl.get_click_action();
        clickAction?.Enable();
        InputSystem.QueueStateEvent(Keyboard.current, new KeyboardState(Key.Space));
        InputSystem.Update();

        Assert.IsTrue(clickAction.triggered);

        btn.OnClicked();

        yield return new WaitForSeconds(1f);
    }

}
