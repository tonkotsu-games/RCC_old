using UnityEngine;

public class PopupDamageController : MonoBehaviour
{
    public static PopupDamageController instance;

    [SerializeField] GameObject popupPrefab;

    private void Awake()
    {
      if(instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }

    public void CreatePopupText(int damage, Transform location)
    {
        GameObject instance = Instantiate(popupPrefab);
        instance.name = "DamagePopup";
        instance.GetComponent<PopupDamage>().AdjustPopUp(damage, location);
        
    }

    
}