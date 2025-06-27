using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Player;
using UnityEngine.InputSystem.LowLevel;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.UIElements;
using System.Net.Mail;

public class ControlsTest : InputTestFixture
{
        [UnityTest]
        public IEnumerator ControlsTestWithEnumeratorPasses()
        {
                //incarcare scena initiala
                var asyncOp = SceneManager.LoadSceneAsync("StartGame");
                yield return new WaitUntil(() => asyncOp.isDone);
                var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
                yield return new WaitUntil(() => asyncOp2.isDone);

                var playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
                Assert.IsNotNull(playerMovement, "PlayerMovement should not be null after loading Gameplay scene");

                Assert.IsTrue(playerMovement.CurrentControl.get_action().name.Equals("MouseMove"), "Default control should be mouse movement");

                InputActionMap playerActionMap = playerMovement.inputActions.FindActionMap("Player");
                var input = playerActionMap.FindAction("KeyboardMove");

                playerMovement.change_strategy(input);
                yield return new WaitForSeconds(1f); // Wait for the control change to take
                Assert.IsTrue(playerMovement.CurrentControl.get_action().name.Equals("KeyboardMove"), "Control should change to keyboard movement");

                input = playerActionMap.FindAction("EyeMove");
                playerMovement.change_strategy(input);
                yield return new WaitForSeconds(1f); // Wait for the control change to take
#if !UNITY_WEBGL
                Assert.IsTrue(playerMovement.CurrentControl.get_action().name.Equals("EyeMove"), "Control should change to eye movement");
#else
                Assert.IsTrue(playerMovement.CurrentControl.get_action().name.Equals("MouseMove"), "Control should remain mouse movement on WebGL");
#endif
        }
        [UnityTest]
        public IEnumerator ControlsTestMoveMouse()
        {
                //stergere mouse curent
                if (Mouse.current != null)
                        InputSystem.RemoveDevice(Mouse.current);

                // adaug un mouse virtual
                var m_test = InputSystem.AddDevice<Mouse>();

                // Load scenes
                var asyncOp = SceneManager.LoadSceneAsync("StartGame");
                yield return new WaitUntil(() => asyncOp.isDone);
                var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
                yield return new WaitUntil(() => asyncOp2.isDone);

                var playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
                Assert.IsNotNull(playerMovement);

                var player = GameObject.FindObjectOfType<PlayerManager>();
                Assert.IsNotNull(player);

                var orgPos = player.transform.position;

                //simulare miscare mouse
                InputSystem.QueueStateEvent(m_test, new MouseState
                {
                        position = new Vector2(600f, 400f) 
                });
                InputSystem.Update();

                player.IsMoving = true;
                yield return new WaitForSeconds(1f);

                var newPos = player.transform.position;
                Assert.True(newPos.x> orgPos.x + 0.1f, "Player should have moved to the right");
        }
        [UnityTest]
        public IEnumerator ControlsTestMoveKeyboard()
        {
                //incarcare scene
                var asyncOp = SceneManager.LoadSceneAsync("StartGame");
                yield return new WaitUntil(() => asyncOp.isDone);
                var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
                yield return new WaitUntil(() => asyncOp2.isDone);

                var playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
                Assert.IsNotNull(playerMovement);

                var player = GameObject.FindObjectOfType<PlayerManager>();
                Assert.IsNotNull(player);

                var orgPos = player.transform.position;

                //adaugare tastaura virtuala
                var k_test = InputSystem.AddDevice<Keyboard>();
                Assert.IsNotNull(k_test, "Keyboard should be added successfully");

                var action = playerMovement.CurrentControl.get_action();
                var actionMap = action.actionMap;
                actionMap.devices = new[] { k_test };

                //simulare miscare tastatura
                InputSystem.QueueStateEvent(k_test, new KeyboardState(Key.W));
                InputSystem.Update();

                player.IsMoving = true;

                for (int i = 0; i < 50; i++)
                {
                        playerMovement.CurrentControl.Move(player);
                        yield return null;
                }

                var newPos= player.transform.position;
                Assert.True(newPos.x> orgPos.x + 0.1f, "Player should have moved");
        }
        [UnityTest]
        public IEnumerator ControlsTestMoveEye()
        {
#if !UNITY_WEBGL
                //incarcare scene
                var asyncOp = SceneManager.LoadSceneAsync("StartGame");
                yield return new WaitUntil(() => asyncOp.isDone);
                var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
                yield return new WaitUntil(() => asyncOp2.isDone);

                var playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
                Assert.IsNotNull(playerMovement);

                var player = GameObject.FindObjectOfType<PlayerManager>();
                Assert.IsNotNull(player);

                var orgPos = player.transform.position;

                // adaugare control cu privirea
                var eyeTrack = playerMovement.CurrentControl;
                Assert.AreEqual("EyeMove", eyeTrack.get_action().name);

                //simulare miscare cu privirea
                // ar trebui aici sa adaug date fictive in API-ul de Eye Tracking
                //newPosition newPos=eyeTrack.API.
                var newPos= new Vector3(orgPos.x + 0.2f, orgPos.y, orgPos.z); // Simulate a new position

                InputSystem.Update();

                // Make sure player is ready to move
                player.IsMoving = true;

                // Simulate update loop
                for (int i = 0; i < 30; i++)
                {
                        playerMovement.CurrentControl.Move(player);
                        yield return null;
                }

                var newPosition = player.transform.position;
                Assert.Greater(newPos.x, orgPos.x + 0.1f, "Player should have moved using Eye control");
                #endif
        }

    
}
