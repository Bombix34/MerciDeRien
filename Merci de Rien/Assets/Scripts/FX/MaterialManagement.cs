using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManagement : MonoBehaviour
{
    bool wasVisible = true;
    GameObject hitObject;

    void Start()
    {
    }

    private void Update()
    {
       IsInView();
    }

    private void IsInView()
    {
        Camera cam = Camera.main;
       //Vector3 pointOnScreen = cam.WorldToScreenPoint(toCheck.GetComponentInChildren<Renderer>().bounds.center);
        RaycastHit hit;

        if (Physics.Linecast(cam.transform.position, gameObject.GetComponentInChildren<Renderer>().bounds.max, out hit))
        {
            if ((hit.transform.name != this.gameObject.name) && (hit.transform.tag == "Tree"))
            {
                if (Physics.Linecast(cam.transform.position, gameObject.GetComponentInChildren<Renderer>().bounds.center, out hit))
                {
                    if ((hit.transform.name != this.gameObject.name)&&(hit.transform.tag == "Tree"))
                    {
                        hitObject = hit.transform.gameObject;
                        if (wasVisible)
                        {
                            hitObject.GetComponent<TreeMaterialManager>().UpdateMaterial(true);
                            wasVisible = false;
                        }
                        return;
                    }
                }
            }
        }
        if (!wasVisible)
        {

            hitObject.GetComponent<TreeMaterialManager>().UpdateMaterial(false);
            hitObject = null;
            wasVisible = true;
        }
    }


}
