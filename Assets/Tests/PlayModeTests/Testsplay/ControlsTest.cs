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
                // Load the initial scene
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
                // Remove the existing system mouse
                if (Mouse.current != null)
                        InputSystem.RemoveDevice(Mouse.current);

                // Add a virtual mouse — this will now become Mouse.current
                var testMouse = InputSystem.AddDevice<Mouse>();

                // Load scenes
                var asyncOp = SceneManager.LoadSceneAsync("StartGame");
                yield return new WaitUntil(() => asyncOp.isDone);
                var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
                yield return new WaitUntil(() => asyncOp2.isDone);

                var playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
                Assert.IsNotNull(playerMovement);

                var player = GameObject.FindObjectOfType<PlayerManager>();
                Assert.IsNotNull(player);

                var originalPosition = player.transform.position;

                // Simulate mouse movement input
                InputSystem.QueueStateEvent(testMouse, new MouseState
                {
                        position = new Vector2(600f, 400f) // Put this over the player to hit the collider
                });
                InputSystem.Update();

                player.IsMoving = true;

                // Trigger a frame so Move() runs and processes the input
                // playerMovement.CurrentControl.Move(player);

                // Wait for movement to happen
                yield return new WaitForSeconds(1f);

                var newPosition = player.transform.position;
                Assert.Greater(newPosition.x, originalPosition.x + 0.1f, "Player should have moved to the right");
        }
        [UnityTest]
        public IEnumerator ControlsTestMoveKeyboard()
        {
                // Load scenes
                var asyncOp = SceneManager.LoadSceneAsync("StartGame");
                yield return new WaitUntil(() => asyncOp.isDone);
                var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
                yield return new WaitUntil(() => asyncOp2.isDone);

                var playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
                Assert.IsNotNull(playerMovement);

                var player = GameObject.FindObjectOfType<PlayerManager>();
                Assert.IsNotNull(player);

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
                InputSystem.Update();

                player.IsMoving = true;

                // Call move repeatedly to simulate frame updates
                for (int i = 0; i < 30; i++)
                {
                        playerMovement.CurrentControl.Move(player);
                        yield return null;
                }

                var newPosition = player.transform.position;
                Assert.Greater(newPosition.x, originalPosition.x + 0.1f, "Player should have moved");
        }
        [UnityTest]
        public IEnumerator ControlsTestMoveEye()
        {
                // Load scenes
                var asyncOp = SceneManager.LoadSceneAsync("StartGame");
                yield return new WaitUntil(() => asyncOp.isDone);
                var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
                yield return new WaitUntil(() => asyncOp2.isDone);

                var playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
                Assert.IsNotNull(playerMovement);

                var player = GameObject.FindObjectOfType<PlayerManager>();
                Assert.IsNotNull(player);

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
                Assert.Greater(newPosition.x, originalPosition.x + 0.1f, "Player should have moved using Eye control");
        }

    
}
