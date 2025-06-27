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
<<<<<<< HEAD
                //incarcare scena initiala
=======
                // Load the initial scene
>>>>>>> origin/fight_Scene
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
<<<<<<< HEAD
                //stergere mouse curent
                if (Mouse.current != null)
                        InputSystem.RemoveDevice(Mouse.current);

                // adaug un mouse virtual
                var m_test = InputSystem.AddDevice<Mouse>();
=======
                // Remove the existing system mouse
                if (Mouse.current != null)
                        InputSystem.RemoveDevice(Mouse.current);

                // Add a virtual mouse — this will now become Mouse.current
                var testMouse = InputSystem.AddDevice<Mouse>();
>>>>>>> origin/fight_Scene

                // Load scenes
                var asyncOp = SceneManager.LoadSceneAsync("StartGame");
                yield return new WaitUntil(() => asyncOp.isDone);
                var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
                yield return new WaitUntil(() => asyncOp2.isDone);

                var playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
                Assert.IsNotNull(playerMovement);

                var player = GameObject.FindObjectOfType<PlayerManager>();
                Assert.IsNotNull(player);

<<<<<<< HEAD
                var orgPos = player.transform.position;

                //simulare miscare mouse
                InputSystem.QueueStateEvent(m_test, new MouseState
                {
                        position = new Vector2(600f, 400f) 
=======
                var originalPosition = player.transform.position;

                // Simulate mouse movement input
                InputSystem.QueueStateEvent(testMouse, new MouseState
                {
                        position = new Vector2(600f, 400f) // Put this over the player to hit the collider
>>>>>>> origin/fight_Scene
                });
                InputSystem.Update();

                player.IsMoving = true;
<<<<<<< HEAD
                yield return new WaitForSeconds(1f);

                var newPos = player.transform.position;
                Assert.True(newPos.x> orgPos.x + 0.1f, "Player should have moved to the right");
=======

                // Trigger a frame so Move() runs and processes the input
                // playerMovement.CurrentControl.Move(player);

                // Wait for movement to happen
                yield return new WaitForSeconds(1f);

                var newPosition = player.transform.position;
                Assert.Greater(newPosition.x, originalPosition.x + 0.1f, "Player should have moved to the right");
>>>>>>> origin/fight_Scene
        }
        [UnityTest]
        public IEnumerator ControlsTestMoveKeyboard()
        {
<<<<<<< HEAD
                //incarcare scene
=======
                // Load scenes
>>>>>>> origin/fight_Scene
                var asyncOp = SceneManager.LoadSceneAsync("StartGame");
                yield return new WaitUntil(() => asyncOp.isDone);
                var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
                yield return new WaitUntil(() => asyncOp2.isDone);

                var playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
                Assert.IsNotNull(playerMovement);

                var player = GameObject.FindObjectOfType<PlayerManager>();
                Assert.IsNotNull(player);

<<<<<<< HEAD
                var orgPos = player.transform.position;

                //adaugare tastaura virtuala
                var k_test = InputSystem.AddDevice<Keyboard>();
                Assert.IsNotNull(k_test, "Keyboard should be added successfully");

                var action = playerMovement.CurrentControl.get_action();
                var actionMap = action.actionMap;
                actionMap.devices = new[] { k_test };

                //simulare miscare tastatura
                InputSystem.QueueStateEvent(k_test, new KeyboardState(Key.W));
=======
                var originalPosition = player.transform.position;

                // Add virtual keyboard
                var testKeyboard = InputSystem.AddDevice<Keyboard>();
                Assert.IsNotNull(testKeyboard);

                // Restrict the input action map to this keyboard (optional but safer)
                var action = playerMovement.CurrentControl.get_action();
                var actionMap = action.actionMap;
                actionMap.Disable();
                actionMap.devices = new[] { testKeyboard };
                actionMap.Enable();

                // Simulate keyboard input
                InputSystem.QueueStateEvent(testKeyboard, new KeyboardState(Key.W));
>>>>>>> origin/fight_Scene
                InputSystem.Update();

                player.IsMoving = true;

<<<<<<< HEAD
                for (int i = 0; i < 50; i++)
=======
                // Call move repeatedly to simulate frame updates
                for (int i = 0; i < 30; i++)
>>>>>>> origin/fight_Scene
                {
                        playerMovement.CurrentControl.Move(player);
                        yield return null;
                }

<<<<<<< HEAD
                var newPos= player.transform.position;
                Assert.True(newPos.x> orgPos.x + 0.1f, "Player should have moved");
=======
                var newPosition = player.transform.position;
                Assert.Greater(newPosition.x, originalPosition.x + 0.1f, "Player should have moved");
>>>>>>> origin/fight_Scene
        }
        [UnityTest]
        public IEnumerator ControlsTestMoveEye()
        {
<<<<<<< HEAD
#if !UNITY_WEBGL
                //incarcare scene
=======
                // Load scenes
>>>>>>> origin/fight_Scene
                var asyncOp = SceneManager.LoadSceneAsync("StartGame");
                yield return new WaitUntil(() => asyncOp.isDone);
                var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
                yield return new WaitUntil(() => asyncOp2.isDone);

                var playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
                Assert.IsNotNull(playerMovement);

                var player = GameObject.FindObjectOfType<PlayerManager>();
                Assert.IsNotNull(player);

<<<<<<< HEAD
                var orgPos = player.transform.position;

                // adaugare control cu privirea
                var eyeTrack = playerMovement.CurrentControl;
                Assert.AreEqual("EyeMove", eyeTrack.get_action().name);

                //simulare miscare cu privirea
                // ar trebui aici sa adaug date fictive in API-ul de Eye Tracking
                //newPosition newPos=eyeTrack.API.
                var newPos= new Vector3(orgPos.x + 0.2f, orgPos.y, orgPos.z); // Simulate a new position
=======
                var originalPosition = player.transform.position;

                // Assuming you have an EyeControl implementing IControl
                var eyeControl = playerMovement.CurrentControl;
                Assert.AreEqual("EyeMove", eyeControl.get_action().name);

                // Add a virtual input device if needed (or fake the action value)
                // Here's a way to simulate the eye position input:
                var gazeAction = eyeControl.get_action();

                // You may need to manually set the action's value
                InputSystem.QueueDeltaStateEvent(
                    gazeAction.controls[0], // assuming this is a Vector2 control
                    new Vector2(originalPosition.x + 200f, originalPosition.y)
                );
>>>>>>> origin/fight_Scene

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
<<<<<<< HEAD
                Assert.Greater(newPos.x, orgPos.x + 0.1f, "Player should have moved using Eye control");
                #endif
=======
                Assert.Greater(newPosition.x, originalPosition.x + 0.1f, "Player should have moved using Eye control");
>>>>>>> origin/fight_Scene
        }

    
}
