﻿using System;
using TMPro;

namespace TextMesh_Pro.Examples___Extras.Scripts
{
    /// <summary>
    /// EXample of a Custom Character Input Validator to only allow digits from 0 to 9.
    /// </summary>
    [Serializable]
    //[CreateAssetMenu(fileName = "InputValidator - Digits.asset", menuName = "TextMeshPro/Input Validators/Digits", order = 100)]
    public class TMPDigitValidator : TMP_InputValidator
    {
        // Custom text input validation function
        public override char Validate(ref string text, ref int pos, char ch)
        {
            if (ch >= '0' && ch <= '9')
            {
                text += ch;
                pos += 1;
                return ch;
            }

            return (char)0;
        }
    }
}
