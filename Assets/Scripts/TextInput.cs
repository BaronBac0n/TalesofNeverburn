using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{

    GameController controller;

    public InputField inputField;

    private void Awake()
    {
        controller = GetComponent<GameController>();
        EventSystem.current.SetSelectedGameObject(inputField.gameObject);
        inputField.ActivateInputField();
        //inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    private void Update()
    {
        if (GameStateController.instance.gameState != GameStateController.GameState.DEAD)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                AcceptStringInput(inputField.text);
            }
        }
    }

    void AcceptStringInput(string userInput)
    {
        if (controller.stateController.gameState != GameStateController.GameState.DEAD)
        {
            userInput = userInput.ToLower();
            controller.LogStringWithReturn(userInput);

            char[] delimiterCharacters = { ' ' };
            string[] seperatedInputWords = userInput.Split(delimiterCharacters);

            for (int i = 0; i < controller.inputActions.Length; i++)
            {
                InputAction inputAction = controller.inputActions[i];
                if (inputAction.keyWord == seperatedInputWords[0])
                {
                    inputAction.RespondToInput(controller, seperatedInputWords);
                }
                //else
                //{
                //    controller.LogStringWithReturn("Not recognised");
                //    break;
                //}
            }

            InputComplete();
        }
    }

    public void InputComplete()
    {
        controller.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
    }
}
