using Discord;
using SleekRender;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnitTrainerMod.Content;

public class CustomInputField : MonoBehaviour
{
    public InputField inputField;
    public Text textField;

    private string initialValue;
    private string lastValue;
    private static ScrollWindow scrollWindowCreatureInfo;
    private static WindowCreatureInfo windowCreatureInfo;
    private Actor currentActor = null;
    private string statName = null;

    private void Start()
    {
        textField.alignment = TextAnchor.MiddleCenter;
        windowCreatureInfo = CanvasMain.FindAnyObjectByType<WindowCreatureInfo>();
        scrollWindowCreatureInfo = ScrollWindow.get("inspect_unit");
    }

    public void Update()
    {
        if (statName == null) {
            return;
        }


        if (currentActor != Config.selectedUnit && Config.selectedUnit != null) {
            currentActor = Config.selectedUnit;
            setText(Config.selectedUnit.stats[statName].ToString());
        }
    }

    private void OnEnable()
    {
        if (!gameObject.GetComponent<InputField>())
        {
            inputField = gameObject.AddComponent<InputField>();
        }
        if (!gameObject.GetComponent<Text>())
        {
            textField = gameObject.AddComponent<Text>();
        }
    }

    public void Init(string pStatName)
    {
        inputField.onValueChanged.AddListener(delegate
        {
            CheckInput(inputField);
        });

        statName = pStatName;
    }

    private void OnDisable()
    {
        inputField.onEndEdit.RemoveAllListeners();
    }

    private void CheckInput(InputField input)
    {
        if (string.IsNullOrEmpty(input.text)) {
            input.text = initialValue;
            lastValue = initialValue;
            setStat(initialValue);

            return;
        }

        try {
            int result = Int32.Parse(input.text);

            if (result <= 999) {
                input.text = result.ToString();
                lastValue = result.ToString();
            } else {
                input.text = lastValue;
            }

            UnitTrainerMod.LogInfo(input.text);
            setStat(input.text);

            return;
        } catch (FormatException) {
            input.text = lastValue;
            setStat(lastValue);

            return;
        }
    }

    private void setStat(string statValue) {
        if (currentActor) {
            int result = Int32.Parse(statValue);

            switch (statName) {
                case "diplomacy":
                    currentActor.data.diplomacy = result;
                    break;
                case "intelligence":
                    currentActor.data.intelligence = result;
                    break;
                case "stewardship":
                    currentActor.data.stewardship = result;
                    break;
                case "warfare":
                    currentActor.data.warfare = result;
                    break;
            }
            
            currentActor.updateStats();
        }
    }

    public void setText(string ptext)
    {
        textField.text = ptext;
        inputField.text = ptext;
        initialValue = ptext;
        lastValue = ptext;
    }
}
