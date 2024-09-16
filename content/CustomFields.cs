using NeoModLoader.General;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UltimateJoystick;

namespace UnitTrainerMod.Content;

internal class CustomFields
{
    //private static Actor actor => Config.selectedUnit;
    private static ScrollWindow scrollWindowCreatureInfo;
    private static WindowCreatureInfo windowCreatureInfo;

    public static void init()
    {
        // Get the creature inspect window - which opens it... - and close it.
        scrollWindowCreatureInfo = ScrollWindow.get("inspect_unit");
        scrollWindowCreatureInfo.hide("left", false);

        CustomFields.addTextField("diplomacy");
        CustomFields.addTextField("warfare");
        CustomFields.addTextField("stewardship");
        CustomFields.addTextField("intelligence");
    }

    private static void addTextField(string parentName)
    {
        // Create a runtime GameObject to hold the Trainer Input Fields
        GameObject inputWrapper = new GameObject();
        inputWrapper.name = parentName + "Wrapper";

        // Add the CustomInputField to the GameObject
        CustomInputField customInputField = inputWrapper.AddComponent<CustomInputField>();

        // Configure TextField
        customInputField.Init(parentName);
        customInputField.setText("0");
        customInputField.textField.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        customInputField.inputField.textComponent = customInputField.textField;


        // Query relevant elements
        Transform statWrapper = scrollWindowCreatureInfo.transform.FindRecursive("StatIcons").FindRecursive(parentName);
        Transform statText = statWrapper.FindRecursive("Text");

        // Add the GameObject to the window
        statText.gameObject.SetActive(false);
        inputWrapper.transform.SetParent(statWrapper);
        inputWrapper.transform.localPosition = new Vector3(0, -4.5f, 0);
        inputWrapper.transform.localScale = new Vector3(0.42f, 0.42f, 0.42f);
    }
}