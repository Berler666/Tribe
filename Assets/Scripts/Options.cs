using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    bool toggleVsync = false;

    public Button VsyncButton;
	

    public void ToggleVSync()
    {
        if(toggleVsync == false)
        {
            QualitySettings.vSyncCount = 1;
            VsyncButton.GetComponent<Image>().color = Color.green;
            toggleVsync = true;
        }
        else if(toggleVsync == true)
        {
            QualitySettings.vSyncCount = 0;
            VsyncButton.GetComponent<Image>().color = Color.white;
            toggleVsync = false;
        }
    }
}
