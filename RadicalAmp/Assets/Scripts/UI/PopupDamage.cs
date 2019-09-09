using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupDamage : MonoBehaviour
{
    [SerializeField] Animator anim;
    TextMeshProUGUI popupText;
    [SerializeField]
    Color[] popUpColors;
    GameObject canvas;

    void OnEnable()
    {
        canvas = Locator.instance.GetCanvas();
        anim = gameObject.GetComponentInChildren<Animator>();
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        popupText = anim.GetComponent<TextMeshProUGUI>();
        
    }

    public void SetText(string text)
    {
        Debug.Log("string to set: " + text);
        popupText.text = text;
    }

    public void SetColor(int damage)
    {
        if(damage<= 400)
        {
            popupText.fontSize = 30f;
            popupText.color = popUpColors[0];
        }
        else if (damage <= 750)
        {
            popupText.fontSize = 60f;
            popupText.color = popUpColors[1];
        }
        else if(damage > 750)
        {
            popupText.fontSize = 150f;
            popupText.color = popUpColors[2];
        }
    }

    public void AdjustPopUp(int damage,Transform location)
    {
        Debug.Log("damage: " + damage);
        SetText(damage.ToString());
        SetColor(damage);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector3(location.position.x + Random.Range(-2f, 2f), location.position.y + Random.Range(-2f, 2f), location.position.z));
        transform.SetParent(canvas.transform, false);
        transform.position = screenPosition;
    }
    
}
       
       
       
       
       
       
       
       
       
       
       