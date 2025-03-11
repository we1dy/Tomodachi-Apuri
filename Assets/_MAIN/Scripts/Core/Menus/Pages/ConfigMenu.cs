using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DIALOGUE;
using UnityEngine.Audio;

public class ConfigMenu : MenuPage
{
    public static ConfigMenu instance {  get; private set; }

    [SerializeField] private GameObject[] panels;
    private GameObject activePanel;

    public UI_ITEMS ui;

    [SerializeField] private AnimationCurve audioFalloffCurve;

    private VN_Configuration config => VN_Configuration.activeConfig; 

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }

        activePanel = panels[0];

        LoadConfig();
    }

    private void LoadConfig()
    {
        if (File.Exists(VN_Configuration.filePath))
            VN_Configuration.activeConfig = FileManager.Load<VN_Configuration>(VN_Configuration.filePath, encrypt: VN_Configuration.ENCRYPT);
        else
            VN_Configuration.activeConfig = new VN_Configuration();

        VN_Configuration.activeConfig.Load();
    }

    private void OnApplicationQuit()
    {
        VN_Configuration.activeConfig.Save();
        VN_Configuration.activeConfig = null;
    }

    public void OpenPanel(string panelName)
    {
        GameObject panel = panels.First(p => p.name.ToLower() == panelName.ToLower());

        if (panel == null)
        {
            Debug.LogWarning($"Did not find panel called '{panelName}' in config menu.");
            return;
        }

        if (activePanel != null && activePanel != panel)
            activePanel.SetActive(false);

        panel.SetActive(true);
        activePanel = panel;
    }

    [System.Serializable]

    public class UI_ITEMS
    {
        private static Color button_selectedColor = new Color(0.58f, 0.52f, 0.52f, 1);
        private static Color button_unselectedColor = new Color(0.97f, 0.92f, 0.92f, 1); 

        private static Color text_selectedColor = new Color(0.97f, 0.92f, 0.92f, 1);
        private static Color text_unselectedColor = new Color(0.24f, 0.21f, 0.32f, 1);

        public static Color musicOnColor = new Color(1f, 0.44f, 0.57f, 1f);
        public static Color musicOnColor2 = new Color(0f, 0.75f, 0.65f, 1f);
        public static Color musicOffColor = new Color(0.58f, 0.52f, 0.52f, 1f); 


        [Header("General")]
        public Button skippingContinue;
        public Button skippingStop;
        public Slider architectSpeed, autoReaderSpeed;

        [Header("Audio")]
        public Slider musicVolume;
        public Image musicFill;
        public Slider sfxVolume;
        public Image sfxFill;
        public Slider voicesVolume;
        public Image voicesFill;
        public Sprite mutedSymbol;
        public Sprite unmutedSymbol;
        public Image musicMute;
        public Image sfxMute;
        public Image voicesMute;

        public void SetButtonColors(Button A, Button B, bool selectedA)
        {
            A.GetComponent<Image>().color = selectedA ? button_selectedColor : button_unselectedColor;
            B.GetComponent<Image>().color = !selectedA ? button_selectedColor : button_unselectedColor;

            A.GetComponentInChildren<TextMeshProUGUI>().color = selectedA ? text_selectedColor : text_unselectedColor;
            B.GetComponentInChildren<TextMeshProUGUI>().color = !selectedA ? text_selectedColor : text_unselectedColor;
        }
    }

    public void SetContinueSkippingAfterChoice(bool continueSkipping)
    {
        config.continueSkippingAfterChoice = continueSkipping;  
        ui.SetButtonColors(ui.skippingContinue, ui.skippingStop, continueSkipping);
    }

    public void SetTextArchitectSpeed()
    {
        config.dialogueTextSpeed = ui.architectSpeed.value;
        DialogueSystem.instance.conversationManager.architect.speed = config.dialogueTextSpeed;
    }

    public void SetAutoReaderSpeed()
    {
        config.dialogueAutoReadSpeed = ui.autoReaderSpeed.value;

        AutoReader autoreader = DialogueSystem.instance.autoReader;
        if (autoreader != null)
            autoreader.speed = config.dialogueAutoReadSpeed;
    }

    public void SetMusicVolume()
    {
        config.musicVolume = ui.musicVolume.value;
        AudioManager.instance.SetMusicVolume(config.musicVolume, config.musicMute);

        ui.musicFill.color = config.musicMute ? UI_ITEMS.musicOffColor : UI_ITEMS.musicOnColor;
    }
    public void SetSFXVolume()
    {
        config.sfxVolume = ui.sfxVolume.value;
        AudioManager.instance.SetSFXVolume(config.sfxVolume, config.sfxMute);

        ui.sfxFill.color = config.sfxMute ? UI_ITEMS.musicOffColor : UI_ITEMS.musicOnColor2;
    }
    public void SetVoicesVolume()
    {
        config.voicesVolume = ui.voicesVolume.value;
        AudioManager.instance.SetVoicesVolume(config.voicesVolume, config.voicesMute);

        ui.voicesFill.color = config.voicesMute ? UI_ITEMS.musicOffColor : UI_ITEMS.musicOnColor;
    }

    public void SetMusicMute()
    {
        config.musicMute = !config.musicMute;
        ui.musicVolume.fillRect.GetComponent<Image>().color = config.musicMute ? UI_ITEMS.musicOffColor : UI_ITEMS.musicOnColor;
        ui.musicMute.sprite = config.musicMute ? ui.mutedSymbol : ui.unmutedSymbol;

        AudioManager.instance.SetMusicVolume(config.musicVolume, config.musicMute);
    }

    public void SetSFXMute()
    {
        config.sfxMute = !config.sfxMute;
        ui.sfxVolume.fillRect.GetComponent<Image>().color = config.sfxMute ? UI_ITEMS.musicOffColor : UI_ITEMS.musicOnColor2;
        ui.sfxMute.sprite = config.sfxMute ? ui.mutedSymbol : ui.unmutedSymbol;

        AudioManager.instance.SetSFXVolume(config.sfxVolume, config.sfxMute);
    }

    public void SetVoicesMute()
    {
        config.voicesMute = !config.voicesMute;
        ui.voicesVolume.fillRect.GetComponent<Image>().color = config.voicesMute ? UI_ITEMS.musicOffColor : UI_ITEMS.musicOnColor;
        ui.voicesMute.sprite = config.voicesMute ? ui.mutedSymbol : ui.unmutedSymbol;

        AudioManager.instance.SetVoicesVolume(config.voicesVolume, config.voicesMute);
    }
}
