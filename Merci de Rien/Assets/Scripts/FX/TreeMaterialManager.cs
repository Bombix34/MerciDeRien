using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMaterialManager : MonoBehaviour
{
    [SerializeField]
    MeshRenderer tronc;
    [SerializeField]
    List<MeshRenderer> feuilles;

    [SerializeField]
    Material troncTransparencyMaterial;
    [SerializeField]
    Material feuillesTransparencyMaterial;


    Material baseTroncMaterial;
    Material baseFeuilleMaterial;

    void Start()
    {
        baseTroncMaterial = tronc.material;
        baseFeuilleMaterial = feuilles[0].material;
    }

    public void UpdateMaterial(bool isTransparency)
    {
        if(isTransparency)
        {
            tronc.material = troncTransparencyMaterial;
            if (feuilles.Count > 0)
            {
                foreach (var item in feuilles)
                {
                    item.material = feuillesTransparencyMaterial;
                }
            }
        }
        else
        {
            tronc.material = baseTroncMaterial;
            if (feuilles.Count > 0)
            {
                foreach (var item in feuilles)
                {
                    item.material = baseFeuilleMaterial;
                }
            }
        }
    }

}
