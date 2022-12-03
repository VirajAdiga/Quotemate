using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
    [SerializeField]
    private Image panel;

    [SerializeField]
    private GameObject text;
    void Start()
    {
        StartCoroutine(IntroAnim());
        StartCoroutine(textAnim());
    }

    IEnumerator textAnim()
    {
        float yPos = text.transform.localPosition.y;
        while (yPos >= -6000)
        {
            yPos -= 100f;
            text.transform.localPosition = new Vector2(0, yPos);
            yield return null;
        }
        while (yPos <= 0)
        {
            yPos += 100;
            text.transform.localPosition = new Vector2(0, yPos);
            yield return null;
        }
    }
    IEnumerator IntroAnim()
    {
        Color32 color = new Color32(0, 0, 0, 0);
        byte alpha = 0;
        while (alpha < 255)
        {
            alpha += 5;
            color = new Color32(0, 0, 0, alpha);
            panel.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("ScrollingText");
    }
}
