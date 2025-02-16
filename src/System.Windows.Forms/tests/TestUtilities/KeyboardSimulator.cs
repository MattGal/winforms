﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using static Interop;
using System.Windows.Forms;

namespace System
{
    public static class KeyboardSimulator
    {
        public static void KeyDown(Control control, Keys key)
        {
            (nint keyCode, nint lParam) = GetKeyParameters(key);
            User32.SendMessageW(control, User32.WM.KEYDOWN, keyCode, lParam);
        }

        public static void KeyPress(Control control, Keys key)
        {
            (nint keyCode, nint lParam) = GetKeyParameters(key);
            User32.SendMessageW(control, User32.WM.KEYDOWN, keyCode, lParam);
            User32.SendMessageW(control, User32.WM.KEYUP, keyCode, lParam);
        }

        private static (nint keyCode, nint lParam) GetKeyParameters(Keys key)
        {
            var keyCode = (nint)key;
            var scanCode = (int)key;
            const int repeatCount = 1;
            nint lParam = PARAM.FromLowHighUnsigned(repeatCount, scanCode);
            return (keyCode , lParam);
        }
    }
}
