using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetection : MonoBehaviour
{
    TerrainData mTerrainData;
    int alphaMapWidth, alphaMapHeight;
    float[,,] mSplatMapData;
    float mNumTextures;

    //pour le son
    float lastFoostep;

    GroundLayer currentPlayerGroundLayer;

    public bool IsPlayingFootstepSound=false;

    void Start()
    {
        if (!IsPlayingFootstepSound)
            return;
        mTerrainData = Terrain.activeTerrain.terrainData;
        alphaMapWidth = mTerrainData.alphamapWidth;
        alphaMapHeight = mTerrainData.alphamapHeight;

        mSplatMapData = mTerrainData.GetAlphamaps(0, 0, alphaMapWidth, alphaMapHeight);
        mNumTextures = mSplatMapData.Length / (alphaMapWidth * alphaMapHeight);

        lastFoostep = 0;
    }

    private Vector3 ConvertToSplatMapCoordinate(Vector3 playerPos)
    {
        Vector3 vecRet = new Vector3();
        Terrain ter = Terrain.activeTerrain;
        Vector3 terPosition = ter.transform.position;
        vecRet.x = ((playerPos.x - terPosition.x) / ter.terrainData.size.x) * ter.terrainData.alphamapWidth;
        vecRet.z = ((playerPos.z - terPosition.z) / ter.terrainData.size.z) * ter.terrainData.alphamapHeight;
        return vecRet;
    }
    
    private void FootstepEvent()
    {
        if (!IsPlayingFootstepSound)
            return;
        int terrainID = GetActiveTerrainTextureIdx();

        switch(terrainID)
        {
            case 0:
                currentPlayerGroundLayer = GroundLayer.shortGrass;
                break;
            case 1:
                currentPlayerGroundLayer = GroundLayer.sand;
                break;
            case 2:
                currentPlayerGroundLayer = GroundLayer.wetSand;
                break;
            case 3:
                currentPlayerGroundLayer = GroundLayer.longGrass;
                break;
            case 6:
                currentPlayerGroundLayer = GroundLayer.agricol;
                break;
            default:
                currentPlayerGroundLayer = GroundLayer.shortGrass;
                break;
        }
        PlayFootstepSound();
    }

    private void PlayFootstepSound()
    {
        switch (currentPlayerGroundLayer)
        {
            case TerrainDetection.GroundLayer.shortGrass:
            default:
                AkSoundEngine.SetSwitch("floor_type", "grass", gameObject);
                AkSoundEngine.SetRTPCValue("grass_height", 1, gameObject);
                break;

            case TerrainDetection.GroundLayer.longGrass:
                AkSoundEngine.SetSwitch("floor_type", "grass", gameObject);
                AkSoundEngine.SetRTPCValue("grass_height", 2, gameObject);
                break;

            case TerrainDetection.GroundLayer.sand:
                AkSoundEngine.SetSwitch("floor_type", "sand", gameObject);
                break;

            case TerrainDetection.GroundLayer.wetSand:
                AkSoundEngine.SetSwitch("floor_type", "water", gameObject);
                break;

            case TerrainDetection.GroundLayer.agricol:
                AkSoundEngine.SetSwitch("floor_type", "mud", gameObject);
                break;
        }

        if (lastFoostep >= 0.25f)
        {
            AkSoundEngine.PostEvent("MC_walk_play", gameObject);
            lastFoostep = 0f;
        }
        //StartCoroutine(FootstepSound());
    }

    //Finalement on a pas besoin d'arrêter les footsteps vu qu'on peut bidouiller l'anim 
    /*
        IEnumerator FootstepSound()
        {
            AkSoundEngine.PostEvent("MC_walk_PH_play", gameObject);
            yield return new WaitForSeconds(0.1f);
            AkSoundEngine.PostEvent("MC_walk_end_PH_play", gameObject);
        }
    */

    private int GetActiveTerrainTextureIdx()
    {
        Vector3 playerPos = transform.parent.position;
        Vector3 TerrainCord = ConvertToSplatMapCoordinate(playerPos);
        int ret = 0;
        float comp = 0f;
        for (int i = 0; i < mNumTextures; i++)
        {
            if (comp < mSplatMapData[(int)TerrainCord.z, (int)TerrainCord.x, i])
                ret = i;
        }
        return ret;
    }


    public enum GroundLayer
    {
        shortGrass,
        longGrass,
        sand,
        wetSand,
        agricol
    }

    private void Update()
    {
        //pour le son
        //c'est pour pas incrémenter inutilement le truc jusqu'à des valeur absolument inutiles
        if (lastFoostep <= 1f)
            lastFoostep += Time.deltaTime;
    }
}
