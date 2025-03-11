using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class VN_Configuration
{
    public static VN_Configuration activeConfig;

    public static string filePath => $"{FilePaths.root}vnconfig.cfg";

    public const bool ENCRYPT = false;

    //GENERAL SETTINGS
    public bool continueSkippingAfterChoice = false;
    public float dialogueTextSpeed = 1f;
    public float dialogueAutoReadSpeed = 1f;

    //AUDIO SETTINGS
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public float voicesVolume = 1f;
    public bool musicMute = false; 
    public bool sfxMute = false; 
    public bool voicesMute = false; 

    public void Load()
    {
        var ui = ConfigMenu.instance.ui;

        //GENERAL SETTINGS

        //set continue after skipping option
        ui.SetButtonColors(ui.skippingContinue, ui.skippingStop, continueSkippingAfterChoice);

        //set the value of the architect and auto reader speed
        ui.architectSpeed.value = dialogueTextSpeed;
        ui.autoReaderSpeed.value = dialogueAutoReadSpeed;

        //set audio mixer volumes
        ui.musicVolume.value = musicVolume;
        ui.sfxVolume.value = sfxVolume; 
        ui.voicesVolume.value = voicesVolume;
        ui.musicMute.sprite = musicMute ? ui.mutedSymbol : ui.unmutedSymbol;
        ui.sfxMute.sprite = sfxMute ? ui.mutedSymbol : ui.unmutedSymbol;
        ui.voicesMute.sprite = voicesMute ? ui.mutedSymbol : ui.unmutedSymbol;
    }

    public void Save()
    {
        FileManager.Save(filePath, JsonUtility.ToJson(this), encrypt: ENCRYPT);
    }
}
