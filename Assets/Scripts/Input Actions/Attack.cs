using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/InputActions/Attack")]
public class Attack : InputAction
{

    public override void RespondToInput(GameController controller, string[] seperatedInputWords)
    {
        //PlayerStats.instance.PlayerAttack();
    }
}
