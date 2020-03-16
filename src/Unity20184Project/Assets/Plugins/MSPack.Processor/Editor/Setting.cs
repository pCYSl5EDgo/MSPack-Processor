// Copyright (c) pCYSl5EDgo. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if UNITY_EDITOR
using UnityEditor;

namespace MSPack.Processor.Unity.Editor
{
    public static class Setting
    {
        private const string MenuItemPath = "Window/MessagePack/MSPackCodeModifier";

        [MenuItem(MenuItemPath)]
        private static void ChangeState()
        {
            bool isChecked = Menu.GetChecked(MenuItemPath);
            isChecked = !isChecked;
            PostBuildHook.Enabled = isChecked;
            Menu.SetChecked(MenuItemPath, isChecked);
        }
    }
}
#endif
