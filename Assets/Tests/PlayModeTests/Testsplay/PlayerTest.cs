using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Player;

public class PlayerTest
{
    [UnityTest]
    public IEnumerator PlayerAnimationsTest()
    {
        var asyncOp = SceneManager.LoadSceneAsync("StartGame");
        yield return asyncOp; // Wait for the scene to load
        SceneManager.LoadScene("Gameplay");
        yield return null; // Wait a frame for the scene to load
        var player = GameObject.FindObjectOfType<PlayerManager>();
        yield return null;
        yield return null;
        player.Animator = player.GetComponent<Animator>();
        Assert.IsNotNull(player.Animator, "Animator should not be null");
        //verificarea miscare dreapta
        Assert.IsTrue(player.isFacingRightAnim, " Player should be facing right at the start");
        Assert.IsTrue(player.Animator.GetCurrentAnimatorStateInfo(0).IsName("running_Clip"), "Animator should be in running state at the start");
        // verificarea miscare stanga
        player.IsFacingRight = false;
        yield return null;
        Assert.IsFalse(player.isFacingRightAnim, "Player should not be facing right after setting isFacingRightAnim to false");
        yield return new WaitForSeconds(1f); // Wait for the animation to update
                                             // Assert.IsTrue(player.Animator.GetCurrentAnimatorStateInfo(0).IsName("run_left"), "Animator should be in running left state after changing isFacingRightAnim");
                                             // verificarea oprire miscare
        player.IsMoving = false;
        Assert.IsFalse(player.IsMoving, "Player should not be moving after setting IsMoving to false");
        Assert.IsTrue(player.Animator.GetCurrentAnimatorStateInfo(0).IsName("PLAYER_IDLE"), "Animator should be in idle state after stopping movement");
        yield return null;
        //verificare obiect nou
        player.NewItem = true;
        yield return null;
        Assert.IsTrue(player.NewItem, "NewItem should be true after setting it to true");
        Assert.IsTrue(player.Animator.GetCurrentAnimatorStateInfo(0).IsName("finiding_item"), "Animator should be in finding item state after setting NewItem to true");
        yield return null;
        //verificare consumare mancare
        player.IsMoving = true;
        player.IsEating = true;
        yield return null;
        Assert.IsTrue(player.IsEating, "IsEating should be true after setting it to true");
        yield return new WaitForSeconds(1f); // Wait for the eating animation to play
        Assert.IsTrue(player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Player_is_eating"), "Animator should be in eating state after setting IsEating to true");
        yield return null;
    }
}
