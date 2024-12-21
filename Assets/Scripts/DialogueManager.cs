using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Singleton

    public TextMeshProUGUI textMessage;
    public AudioSource soundSource;
    public RawImage background;
    public GameObject displayMessage;
    public Coroutine currentCoroutine; // Coroutine to zarz¹dzania aktywnym tekstem

    private Queue<string> messageQueue = new Queue<string>(); // Kolejka puszczanych wiadomosci po u¿yciu CommentDelayMessage
    public bool isMessageDisplaying = false; // Sprawdza, czy jest aktualnie wyœwietlana wiadomoœæ

    void Awake()
    {
        // Ustawia na start singletona jako instancjê
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Tego u¿ywamy jako pierwsz¹ wiadomoœc w kolejce. To tworzy kolejkê aktualnych wiadomoœci, któr¹ kontynuujemy poprzez u¿yhcie CommentDelayMessage
    public void CommentMessage(string message)
    {
        // Na start czyœcimy kolejke, niepotrzebne jakoœ bardzo, ale na wszelki.
        messageQueue.Clear();
        if (!isMessageDisplaying)
        {
            messageQueue.Enqueue(message);

        // Jak nie ma ¿adnej innej wiadomoœci wyœwietlanej to zaczyna wyœwietlaæ aktualn¹ kolejklê wiadomoœci
        if (!isMessageDisplaying)
        {
            StartCoroutine(DisplayNextMessage());
        }
        }
    }

    private IEnumerator DisplayNextMessage()
    {
        while (messageQueue.Count > 0)
        {
            isMessageDisplaying = true;
            string message = messageQueue.Dequeue(); // Kolejna wiadomoœc w kolejce

            // Wywo³uje korutyne, meessage to wiadomoœæ a na drugim miejscu czas wyœwietlania wiadomoœci
            yield return StartCoroutine(DisplayMessageRoutine(message, 2f));
        }
        isMessageDisplaying = false; // Kasujemy wyœwietlanie
    }

    // wywo³anie DelayedCommentMessageRoutine
    public void CommentDelayMessage(string message)
    {
        StartCoroutine(DelayedCommentMessageRoutine(message));
    }

    private IEnumerator DelayedCommentMessageRoutine(string message)
    {
        // Delayed czeka na zakoñczenie aktualnej korutyny
        if (currentCoroutine != null)
        {
            yield return currentCoroutine;
        }

        messageQueue.Enqueue(message);

        // Jak nie ma ¿adnej innej wiadomoœci wyœwietlanej to zaczyna wyœwietlaæ aktualn¹ kolejklê wiadomoœci
        if (!isMessageDisplaying)
        {
            StartCoroutine(DisplayNextMessage());
        }
    }

    // Ca³a obs³uga korutyny, Undertale-like bo lubie taki styl dialogu xdd
    private IEnumerator DisplayMessageRoutine(string message, float displayTime)
    {
        textMessage.text = "";
        textMessage.alpha = 1;
        displayMessage.SetActive(true);
        background.color = new Color(background.color.r, background.color.g, background.color.b, 1);

        // I cyk jeb jeb jeb z dzidy laserowej litera po literze
        foreach (char c in message)
        {
            textMessage.text += c;
            soundSource.pitch = Random.Range(0.9f, 1f); // ¿eby nie by³o monotonnie to losujemy pitch, nie du¿o bo kurwa uszy bol¹ xdddd
            soundSource.Play();
            if (c.Equals(',') || c.Equals('.') || c.Equals('?') || c.Equals('!'))
            {
                yield return new WaitForSeconds(0.4f);
            }
            else yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(displayTime);
        

        // No i po zakoñczeniu p³ynny lerpik do przeŸroczystoœci
        float elapsedTime = 0f;
        float fadeDuration = 0.2f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            textMessage.alpha = alpha;
            background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // a tu zerujemy ju¿ tekst i wy³¹czamy aktywnoœæ wiadomoœci.
        displayMessage.SetActive(false);
        textMessage.text = "";
    }
}
