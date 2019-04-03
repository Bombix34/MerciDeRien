using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManagement : MonoBehaviour
{
    public SkinnedMeshRenderer render;
    Material[] objectBaseMaterials;

    public Material silhouettedMaterial;

    bool wasVisible = true;

    void Start()
    {
        objectBaseMaterials = render.materials;
    }

    private void Update()
    {
        IsInView();
    }

    public void ResetObjectMaterials()
    {
        render.materials = objectBaseMaterials;
    }

    public void SetSilhouettedMaterial()
    {
        Material[] final = new Material[render.materials.Length];
        for (int i = 0; i < render.materials.Length; i++)
        {
            final[i] = silhouettedMaterial;
        }
        render.materials = final;
    }

    private void IsInView()
    {
        Camera cam = Camera.main;
       //Vector3 pointOnScreen = cam.WorldToScreenPoint(toCheck.GetComponentInChildren<Renderer>().bounds.center);
        RaycastHit hit;

        if (Physics.Linecast(cam.transform.position, gameObject.GetComponentInChildren<Renderer>().bounds.max, out hit))
        {
            if (hit.transform.name != gameObject.name)
            {
                if (Physics.Linecast(cam.transform.position, gameObject.GetComponentInChildren<Renderer>().bounds.center, out hit))
                {
                    if (hit.transform.name != gameObject.name)
                    {
                        hitObject = hit.transform.gameObject;
                        if (wasVisible)
                        {
                            SetSilhouettedMaterial();
                           // SetHitObjectTransparency(true);
                            wasVisible = false;
                        }
                        return;
                    }
                }
            }
        }
        if (!wasVisible)
        {
            ResetObjectMaterials();

            //SetHitObjectTransparency(false);
            hitObject = null;
            wasVisible = true;
        }
    }

    GameObject hitObject;

    public void SetHitObjectTransparency(bool isTransparency)
    {
        //TEST DE METTRE LES AUTRS OBJETS TRANSPARENTS PLUTOT QUE LE JOUEUR EN SILHOUETTE
        Color baseColor = hitObject.GetComponent<Renderer>().material.color;
        Debug.Log(baseColor);
        if(isTransparency)
            hitObject.GetComponent<Renderer>().material.color = new Color(baseColor.r, baseColor.g,baseColor.b, 0.4f);
        else
            hitObject.GetComponent<Renderer>().material.color = new Color(baseColor.r, baseColor.g, baseColor.b, 1f);
    }


}
