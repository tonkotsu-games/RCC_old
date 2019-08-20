using UnityEngine;
using UnityEngine.UI;

public class PopupDamage : MonoBehaviour
{
    Animator anim;
    Text popupText;

    void OnEnable()
    {
        anim = GameObject.FindWithTag("PopupDamage").GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        popupText = anim.GetComponent<Text>();
    }

    public void SetText(string text)
    {
        popupText.text = text;
    }
}