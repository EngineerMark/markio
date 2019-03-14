using UnityEngine;
using System.Collections.Generic;
using System;

namespace GameInput
{
    public class ControlScheme
    {
        private static string[] possibleInputs;

        public Dictionary<string, KeyCode> keyInput;
        public Dictionary<string, string> axisInput;

        public ControlScheme()
        {
            if (possibleInputs == null)
                possibleInputs = new string[]
                {
                    "Left",
                    "Right",
                    "Jump",
                    "Shoot",
                };

            keyInput = new Dictionary<string, KeyCode>();
            axisInput = new Dictionary<string, string>();

            Debug.Log("ControlScheme created: " + keyInput + ", " + axisInput);
        }

        public void Update()
        {

        }

        public void SetKeys(Dictionary<string, KeyCode> keys)
        {
            keyInput = keys;
        }

        public void SetAxes(Dictionary<string, string> axes)
        {
            axisInput = axes;
        }

        public Dictionary<string, KeyCode> GetKeys()
        {
            return keyInput;
        }
    }
}
