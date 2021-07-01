using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreloadUIManager : MonoBehaviour
{
    public Text progressTest;//进度

    // Update is called once per frame
    void Update()
    {
        UpdateProgressBar(ResMgr.GetInstance().ReportProgress());
    }
    private void UpdateProgressBar(float percentComplete)
    {

        if (progressTest == null) return;
        if (percentComplete >= 0.99f)
        {
            //Destroy(progressTest.gameObject);
            progressTest.text = Mathf.CeilToInt(percentComplete * 100f) + "%";
            return;
        }
        else
        {
            progressTest.text = Mathf.CeilToInt(percentComplete * 100f) + "%";
        }

    }
}
