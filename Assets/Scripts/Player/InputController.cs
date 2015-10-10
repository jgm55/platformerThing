using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class InputController : MonoBehaviour {

    PlayerController player;
    GamePadState state;
    GamePadState prevState;

    float MOVE_THRESHOLD = .6f;

    PlayerIndex playerIndex = 0;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        GamePadState testState = GamePad.GetState(playerIndex);
        if (testState.IsConnected)
        {
            //dont start until controller connected
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Get Actions/ input
        if (GamePad.GetState(playerIndex).IsConnected) {
            prevState = state;
            state = GamePad.GetState(playerIndex);
            // Detect if a button was pressed this frame

            if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            {
                player.jump();
            }

            if (Mathf.Abs(state.ThumbSticks.Left.X) > MOVE_THRESHOLD)
            {
                player.setWalking(state.ThumbSticks.Left.X);
            }
            else
            {
                player.setNotWalking();
            }

            if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed)
            {
                player.changeColor(Color.red);
            }

            if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed)
            {
                player.changeColor(Color.blue);
            }

            if (prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed)
            {
                player.changeColor(Color.yellow);
            }

            if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            {
                player.changeColor(Color.green);
            }

            if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed)
            {
                player.respawn();
            }
        }
	}
}
