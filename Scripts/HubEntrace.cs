using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HubEntrace : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Canvas gameCanvas;
    [SerializeField] int entranceIndex; //if 0 then exit
    [SerializeField] float timeOfFade;
    [SerializeField] int coinsInLevel;
    [SerializeField] Color highlightColor;
    [SerializeField] List<GameObject> effects;
    Coroutine displayCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("levelsCompleted") == entranceIndex - 1)
        {
            GetComponent<SpriteRenderer>().color = highlightColor;
            foreach(SpriteRenderer childSprite in GetComponentsInChildren<SpriteRenderer>())
            {
               Color color = childSprite.color;
               color.a = 1;
               childSprite.color = color;
            }
        }
    }

    private void EnableButtons(bool enable)
    {
        foreach (Button button in canvas.transform.GetComponentsInChildren<Button>())
        {
            button.interactable = enable;
        }
    }

    private void StopProcessingDisplayCanvas()
    {
        if (displayCanvas != null)
        {
            StopCoroutine(displayCanvas);
        }
    }

    public void HideCanvas()
    {
        displayCanvas = StartCoroutine(DisplayCanvas(canvas, 0f, false));
    }

    public IEnumerator DisplayCanvas(Canvas canvas, float endAlpha, bool displayCanvas)
    {
        gameCanvas.GetComponent<CanvasGroup>().alpha = Convert.ToInt32(!displayCanvas);
        StopProcessingDisplayCanvas();
        EnableButtons(displayCanvas);
        var repeats = 60; //frames
        var currentAlpha = canvas.GetComponent<CanvasGroup>().alpha;
        var alphaChange = (endAlpha - currentAlpha) / repeats / timeOfFade;
        for(int i = 0; i < repeats; i++)
        {
            canvas.GetComponent<CanvasGroup>().alpha += alphaChange;
            yield return new WaitForSeconds(timeOfFade / repeats);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(FindObjectOfType<EndGameButton>() != null)
        {
            FindObjectOfType<EndGameButton>().DestroyInstantly();
        }
        displayCanvas = StartCoroutine(DisplayCanvas(canvas, 1f, true));
        canvas.GetComponentInChildren<DisplayCoinsHub>().ChangeText(entranceIndex, coinsInLevel);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        displayCanvas = StartCoroutine(DisplayCanvas(canvas, 0f, false));
    }

}
