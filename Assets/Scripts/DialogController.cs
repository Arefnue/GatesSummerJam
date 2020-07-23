using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Managers;

public class DialogController : MonoBehaviour
{
    [Range(0.5f, 5f)]
    public float waitTimeForSkip = 1f;
    [Range(1, 10f)]
    public int messageSlownessSpeed = 3;
    public Sprite character1, character2;
    public string character1Name, character2Name;
    public List<string> conversations = new List<string>();
    public List<AudioClip> conversationsAudio = new List<AudioClip>();
    public GameObject dialogBoxPopupGO;
    public TextMeshProUGUI avatarName;
    public TextMeshProUGUI textArea;
    public Image avatarSpriteRenderer;
    public GameObject spiritObject;
    public AudioSource musicSource;


    // States
    private bool isConversationActive = false;
    private bool canOpenConversation = true;
    private int activeConversationIndex = 0;
    private float timer = 0f;

    private bool isMessageTyping = false;
    private string fullTypingMessage = "";
    private int messageCharTypedCnt = 0; // to control speed, by skipping every some frames
    
    
    private void Update()
    {
        if(!canOpenConversation && !isConversationActive) { return; }

        timer += Time.unscaledDeltaTime; // Unscaled since Time.ScaleTime is 0

        if (isConversationActive && Input.GetMouseButton(0))
        {
            AttemptSkipMessage();
        }
        else if (isMessageTyping)
        {
            ContinueTypingMessage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canOpenConversation)
        {
            canOpenConversation = false;
            StartConversation();
        }
    }

    private void StartConversation()
    {
        Time.timeScale = 0;

        isConversationActive = true;
        dialogBoxPopupGO.SetActive(true);

        ShowNextConversationMessage();
    }

    /**
     * Show next message from conversation
     * with avatar and name of the speaker
     */
    private void ShowNextConversationMessage()
    {
        if( activeConversationIndex>=conversations.Count)
        {
            TerminateConversation();
            return;
        }

        Sprite sprite = character1;
        string name = character1Name;
        string message = conversations[activeConversationIndex];

        if(activeConversationIndex % 2 == 1) {
            sprite = character2;
            name = character2Name;
        }

        avatarName.text = name;
        avatarSpriteRenderer.sprite = sprite;
        textArea.text = "";

        fullTypingMessage = message;
        isMessageTyping = true;

        SpeakConversationMessage();

        activeConversationIndex++;
    }

    private void SpeakConversationMessage()
    {
        musicSource.enabled = false;
        AudioManager.Manager.PlayConversationSound(conversationsAudio[activeConversationIndex]);
    }

    /**
     * Types message character by character
    */
    private void ContinueTypingMessage()
    {
        messageCharTypedCnt++;
        if (messageCharTypedCnt % messageSlownessSpeed != 0) { return; } // Speed control

        string curMessage = textArea.text;
        if(curMessage.Length == fullTypingMessage.Length)
        {
            isMessageTyping = false;
            return;
        }
        
        curMessage += fullTypingMessage[curMessage.Length];

        textArea.text = curMessage;
    }

    private void AttemptSkipMessage()
    {
        // Typing message will get full text
        if (isMessageTyping && timer>0.5f)
        {
            textArea.text = fullTypingMessage;
            timer = waitTimeForSkip/2;
            isMessageTyping = false;
        }
        // Skipping to next message
        else if(timer>waitTimeForSkip)
        {
            timer = 0;
            ShowNextConversationMessage();
        }
    }

    private void TerminateConversation()
    {
        Time.timeScale = 1;
        Destroy(spiritObject);
        isConversationActive = false;
        dialogBoxPopupGO.SetActive(false);
        musicSource.enabled = true;
        AudioManager.Manager.StopConversationSound();
    }
}
