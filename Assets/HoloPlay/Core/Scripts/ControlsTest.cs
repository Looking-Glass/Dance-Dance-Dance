using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloPlay;

namespace HoloPlay
{
    namespace Extras
    {
        public class ControlsTest : MonoBehaviour
        {
            void OnGUI()
            {
                GUI.skin.box.fontSize = 50;

                var buttons = new Dictionary<ButtonType, string>{
                    { ButtonType.ONE, "Square / 1" },
                    { ButtonType.TWO, "Left / 2" },
                    { ButtonType.THREE, "Right / 3" },
                    { ButtonType.FOUR, "Circle / 4" },
                    { ButtonType.HOME, "Home / 5" },
                };

                foreach (var b in buttons)
                {
                    if (Buttons.GetButton(b.Key))
                        GUILayout.Box(b.Value);
                }
            }
        }
    }
}