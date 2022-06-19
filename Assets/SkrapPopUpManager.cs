using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkrapPopUpManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject damagePopUP;
    private static SkrapPopUpManager instance;

    public static SkrapPopUpManager Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    public IEnumerator SkrapIndicator(int damage)
    {
        float seconds = 1f;
        float distance = 5;
        float fadeSpeed = 1f;
        GameObject go = Instantiate(damagePopUP, GameManager.instance.player.transform.position + new Vector3(0, 5, 0), Quaternion.Euler(90, 0, 0));
        TextMeshProUGUI tmp;
        tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = "+" + damage.ToString();
        Color color;
        color = tmp.color;
        Vector3 pushback = new Vector3(distance, distance, 0);
        float pushbackFalloffSpeed = 20f;

        while (seconds > 0)
        {
            Vector3 positionChange = Vector3.Lerp(pushback, Vector3.zero, pushbackFalloffSpeed * Time.deltaTime) * Time.deltaTime;
            go.transform.position += positionChange;
            yield return new WaitForEndOfFrame();
            seconds -= Time.deltaTime;

        }
        while (seconds <= 0)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            tmp.color = color;
            yield return null;
            if (tmp.color.a <= 0f)
            {
                Destroy(go);
            }
        }

    }
}
