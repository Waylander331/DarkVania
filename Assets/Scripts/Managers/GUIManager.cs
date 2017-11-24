using UnityEngine;
// Not used for now
public class GUIManager : MonoBehaviour {

    public static GUIManager GuiM = null;


    private void Awake()
    {
        if(GuiM == null)
        {
            GuiM = this;
        }
        else if(GuiM != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
}
