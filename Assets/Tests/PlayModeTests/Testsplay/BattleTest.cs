using System.Collections;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Battle;
using Player;
using UnityEditor.SceneManagement;
using System.Runtime.Serialization.Formatters;


public class BattleTest
{
    [UnityTest]
    public IEnumerator BattleTestWithEnumeratorPasses()
    {

        var asyncOp = SceneManager.LoadSceneAsync("StartGame");
        yield return new WaitUntil(() => asyncOp.isDone);
        var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
        yield return new WaitUntil(() => asyncOp2.isDone);

        // Așteaptă un frame pentru ca Start() să ruleze
        yield return null;

        var player = GameObject.FindObjectOfType<PlayerManager>();
        Assert.IsNotNull(player, "PlayerManager should not be null after loading Gameplay scene");

        var gameManager = GameObject.FindObjectOfType<GameController>();
        Assert.IsNotNull(gameManager, "GameController should not be null");

        var enemy = GameObject.Find("Skeleton_first");
        Assert.IsNotNull(enemy, "Enemy should not be null after loading Gameplay scene");

        // Apelează start_battle ca o coroutina completă
        yield return player.start_battle(enemy);

        // Așteaptă ca GameState să se schimbe
        yield return new WaitUntil(() => gameManager.state == GameState.Battle);
        var battleCamera = GameObject.Find("BattleCamera");
        Assert.True(battleCamera.activeSelf, "BattleCamera should be active during battle");
        var playerCamera = GameObject.Find("PlayerCamera");
        Assert.False(playerCamera.activeSelf, "PlayerCamera should be not active during battle");

        // Verifică dacă sistemul de bătălie este activat
        var battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        Assert.IsNotNull(battleSystem, "BattleSystem should not be null during battle");
        //Assert.IsTrue(battleSystem.gameObject.activeSelf, "BattleSystem should be active during battle");
        // Verifică dacă playerul are un animator și este în stare de luptă
        Assert.IsNotNull(battleSystem.EnemyUnit);
        Assert.IsNotNull(battleSystem.Player, "PlayerUnit should not be null in BattleSystem");
        Assert.IsNotNull(battleSystem.HpBarPlayer, "HpBarPlayer should not be null in BattleSystem");
        Assert.IsNotNull(battleSystem.HpBar, "HpBarEnemy should not be null in BattleSystem");
        Assert.IsFalse(gameManager.audioIdle.isPlaying, "Idle audio should not be playing before battle starts");
        Assert.IsTrue(gameManager.audioBattle.isPlaying, "Battle audio should be playing after battle starts");
    }

    [UnityTest]
    public IEnumerator BattleExitEnemyDead()
    {
        var asyncOp = SceneManager.LoadSceneAsync("StartGame");
        yield return new WaitUntil(() => asyncOp.isDone);
        var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
        yield return new WaitUntil(() => asyncOp2.isDone);

        // Așteaptă un frame pentru ca Start() să ruleze
        yield return null;

        var player = GameObject.FindObjectOfType<PlayerManager>();
        Assert.IsNotNull(player, "PlayerManager should not be null after loading Gameplay scene");

        var gameManager = GameObject.FindObjectOfType<GameController>();
        Assert.IsNotNull(gameManager, "GameController should not be null");

        var enemy = GameObject.Find("Skeleton_first");
        Assert.IsNotNull(enemy, "Enemy should not be null after loading Gameplay scene");

        // Apelează start_battle ca o coroutine completă
        yield return player.start_battle(enemy);

        // Așteaptă ca GameState să se schimbe
        yield return new WaitUntil(() => gameManager.state == GameState.Battle);

        // Verifică dacă playerul are un animator și este în stare de luptă
        var battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        Assert.IsNotNull(battleSystem, "BattleSystem should not be null during battle");

        battleSystem.EnemyUnit.Hp = 0;
        battleSystem.State = BattleState.EnemyDead;
        yield return new WaitUntil(() => battleSystem.State == BattleState.EnemyDead);
        yield return new WaitUntil(() => gameManager.state == GameState.FreeRoam);
        Assert.IsFalse(battleSystem.isActiveAndEnabled, " BattleSystem should be inactive after battle ends");
        var playerCamera = GameObject.Find("PlayerCamera");
        Assert.True(playerCamera.activeSelf, "PlayerCamera should be active during free roam");

    }
    [UnityTest]
    public IEnumerator BattleExitPlayerDead()
    {
        var asyncOp = SceneManager.LoadSceneAsync("StartGame");
        yield return new WaitUntil(() => asyncOp.isDone);
        var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
        yield return new WaitUntil(() => asyncOp2.isDone);

        // Așteaptă un frame pentru ca Start() să ruleze
        yield return null;

        var player = GameObject.FindObjectOfType<PlayerManager>();
        Assert.IsNotNull(player, "PlayerManager should not be null after loading Gameplay scene");

        var gameManager = GameObject.FindObjectOfType<GameController>();
        Assert.IsNotNull(gameManager, "GameController should not be null");

        var enemy = GameObject.Find("Skeleton_first");
        Assert.IsNotNull(enemy, "Enemy should not be null after loading Gameplay scene");

        // Apelează start_battle ca o coroutine completă
        yield return player.start_battle(enemy);

        // Așteaptă ca GameState să se schimbe
        yield return new WaitUntil(() => gameManager.state == GameState.Battle);

        // Verifică dacă playerul are un animator și este în stare de luptă
        var battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        Assert.IsNotNull(battleSystem, "BattleSystem should not be null during battle");

        battleSystem.Player.Hp = 0;
        battleSystem.State = BattleState.PlayerDead;
        yield return new WaitUntil(() => battleSystem.State == BattleState.PlayerDead);
        yield return new WaitUntil(() => gameManager.state == GameState.FreeRoam);
        yield return null;
        yield return new WaitForSeconds(1f); // Așteaptă un timp pentru a permite tranziția către GameOver
        Assert.IsTrue(SceneManager.GetActiveScene().name == "GameOver", "GameOver scene should be active after player death");
        Assert.IsNull(GameObject.FindAnyObjectByType<PlayerManager>(), "PlayerManager should be destroyed after player death");
    }

    [UnityTest]
    public IEnumerator BattleTestUnknownMove()
    {
        var asyncOp = SceneManager.LoadSceneAsync("StartGame");
        yield return new WaitUntil(() => asyncOp.isDone);
        var asyncOp2 = SceneManager.LoadSceneAsync("Gameplay");
        yield return new WaitUntil(() => asyncOp2.isDone);
         // Așteaptă un frame pentru ca Start() să ruleze
        yield return null;

        var player = GameObject.FindObjectOfType<PlayerManager>();
        Assert.IsNotNull(player, "PlayerManager should not be null after loading Gameplay scene");

        var gameManager = GameObject.FindObjectOfType<GameController>();
        Assert.IsNotNull(gameManager, "GameController should not be null");

        var enemy = GameObject.Find("Skeleton_first");
        Assert.IsNotNull(enemy, "Enemy should not be null after loading Gameplay scene");

        // Apelează start_battle ca o coroutine completă
        yield return player.start_battle(enemy);

        // Așteaptă ca GameState să se schimbe
        yield return new WaitUntil(() => gameManager.state == GameState.Battle);
        var battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        Assert.IsNotNull(battleSystem, "BattleSystem should not be null during battle");
        yield return new WaitUntil(() => battleSystem.State == BattleState.PlayerAction);
        battleSystem.PlayerActionMove("UnknownMove");
        yield return new WaitForSeconds(4f); // Așteaptă un timp pentru a permite tranziția
           // Assert.IsTrue(battleSystem.State == BattleState.EnemyMove, "Battle state should be EnemyMove after using an unknown move");
    }
}
