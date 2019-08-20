using UnityEngine;

public class PopupDamageController : MonoBehaviour
{
    public static PopupDamageController instance;

    [SerializeField] GameObject popupPrefab;
    private static PopupDamage popupDamageText;
    private static GameObject canvas;

    private void Awake()
    {
      if(instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }
    public void Start()
    {

        popupDamageText = popupPrefab.GetComponent<PopupDamage>();

        canvas = GameObject.FindWithTag("Canvas");
    }

    public void CreatePopupText(string text, Transform location)
    {
        PopupDamage instance = Instantiate(popupDamageText);
        instance.SetText(text);
        instance.name = "DamagePopup";
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
    }
}