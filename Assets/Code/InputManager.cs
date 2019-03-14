using System.Collections.Generic;
using UnityEngine;
using GameInput;

public class InputManager : Manager
{
    private static ControlScheme[] controlScheme;

    public InputManager()
    {
        ControlScheme player1 = new ControlScheme();
        ControlScheme player2 = new ControlScheme();

        player1.SetKeys(new Dictionary<string, KeyCode>()
            {
                {"Left",KeyCode.A},
                {"Right",KeyCode.D},
                {"Jump",KeyCode.W},
                {"Shoot",KeyCode.S},
            }
        );

        player1.SetAxes(new Dictionary<string, string>()
            {
            {"Horizontal","horizontal"},
            {"Jump","a"},
            {"Shoot","b"}
            }
        );

        player2.SetKeys(new Dictionary<string, KeyCode>()
            {
                {"Left",KeyCode.LeftArrow},
                {"Right",KeyCode.RightArrow},
                {"Jump",KeyCode.UpArrow},
                {"Shoot",KeyCode.DownArrow},
             }
        );

        player2.SetAxes(new Dictionary<string, string>()
            {
            {"Horizontal","horizontal"},
            {"Jump","a"},
            {"Shoot","b"}
            }
        );

        controlScheme = new ControlScheme[]{
          player1,
          player2,
        };
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    void Pause()
    {

    }

    public static ControlScheme[] GetControlSchemes(){
      return controlScheme;
    }
}
