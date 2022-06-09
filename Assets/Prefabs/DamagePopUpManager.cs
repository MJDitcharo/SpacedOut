using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUpManager : MonoBehaviour
{
    [SerializeField] GameObject damagePopUP;
    private static DamagePopUpManager instance;

    public static DamagePopUpManager Instance
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
    public IEnumerator DamageIndicator(int damage, Vector3 position)
    {
        float seconds = 0.7f;
        float distance = 5;
        float fadeSpeed = 1f;
        GameObject go = Instantiate(damagePopUP, position + new Vector3(0, 5, 0), Quaternion.Euler(90, 0, 0));
        TextMeshProUGUI tmp;
        tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = "-" + damage.ToString();
        Color color;
        color = tmp.color;
        Vector3 pushback = new Vector3(distance, distance, 0);
        float pushbackFalloffSpeed = 10f;

        while (seconds > 0)
        {
            position = Vector3.Lerp(position, position + pushback, pushbackFalloffSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            distance *= 0.5f;
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
