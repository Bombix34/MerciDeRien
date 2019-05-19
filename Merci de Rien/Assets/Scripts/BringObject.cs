using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CielaSpike;
using System.Threading;

public class BringObject : InteractObject
{
    [SerializeField]
    ObjectReglages reglages;

    [SerializeField]
    PnjManager.CharacterType characterOwner = PnjManager.CharacterType.none;

    Rigidbody body;
    float mass;

    bool isLaunch = false;

    Vector3 baseScale;

    public GameObject explosionParticles;

    protected override void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody>();
        mass = body.mass;
        if (characterOwner == PnjManager.CharacterType.none)
            CanTakeObject = true;
    }

    protected override void UpdateFeedbackInteraction(bool isOn)
    {
        if (feedbackInteraction == null)
            return;
        feedbackInteraction.SetActive(isOn);
        if (isOn)
        {
            if (EventManager.Instance.GetPlayer() != null)
            {
                RectTransform textPosition = textContainer.GetComponent<RectTransform>();
                float playerPositionX = EventManager.Instance.GetPlayer().transform.position.x;
                float result = this.transform.position.x - playerPositionX;

                if (result < 0)
                {
                    //joueur a droite
                    if (textPosition.localPosition.x > 0)
                        textPosition.localPosition = new Vector3(-1 * textPosition.localPosition.x, textPosition.localPosition.y, textPosition.localPosition.z);
                }
                else
                {
                    //joueur a gauche
                    if (textPosition.localPosition.x < 0)
                        textPosition.localPosition = new Vector3(-1 * textPosition.localPosition.x, textPosition.localPosition.y, textPosition.localPosition.z);
                }
            }
            if ((!CanTakeObject) || (GetComponent<BringObject>() != null && GetComponent<BringObject>().GetCharacterOwner() != PnjManager.CharacterType.none))
                textContainer.text = GetInteractText(true);
            else
                textContainer.text = GetInteractText(false);
        }
    }

    public void ResetMass()
    {
        StartCoroutine(PoseObject());
    }

    IEnumerator PoseObject()
    {
        yield return new WaitForSeconds(0.3f);
        body.mass = mass;
    }

    public void LaunchObject()
    {
        isLaunch = true;
        body.freezeRotation = false;
    }

    public ObjectReglages GetObjectReglages()
    {
        return reglages;
    }

    public PnjManager.CharacterType GetCharacterOwner()
    {
        return characterOwner;
    }


    void OnCollisionEnter(Collision collision)
    {
        //quand l'objet est touché par un autre objet lancé
        if (collision.gameObject.tag == "BringObject")
        {
            BringObject otherObject = collision.gameObject.GetComponent<BringObject>();
            if(otherObject.GetObjectReglages().IsBreakingThings&&otherObject.isLaunch)
                StartBreaking();
        }
        //quand on lance l'objet
        if (!isLaunch)
            return;
        StartBreaking();
        if (collision.gameObject.tag=="PNJ")
        {
            PnjManager pnj = collision.gameObject.GetComponent<PnjManager>();
            StartHurting(pnj);
        }
    }

    public PnjManager.CharacterType GetOwner()
    {
        return characterOwner;
    }

    public void StartHurting(PnjManager pnj)
    {
        if (!reglages.IsHurting)
            return;
        //event____________
        pnj.HurtingEvent();
    }

    public void StartBreaking()
    {
        if (!reglages.IsBreaking)
            return;
        //SFX
        AkSoundEngine.PostEvent("ENV_pot_break_play", gameObject);
        //event
        EventManager.Instance.GetDatas().UpdateCharacterEvent(EventDatabase.EventType.brokeObjectsTotal, characterOwner, 1);

        this.StartCoroutineAsync(Explode());
    }

    //SOURCE : https://github.com/unitycoder/SimpleMeshExploder

    IEnumerator Explode()
    {
        yield return Ninja.JumpToUnity;
        GetComponent<MeshRenderer>().enabled = false;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;
        Vector2[] uvs = mesh.uv;
        int index = 0;

        if(explosionParticles!=null)
        {
            GameObject particle = Instantiate(explosionParticles, transform.position, Quaternion.identity) as GameObject;
            particle.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        }

        // remove collider from original
        GetComponent<Collider>().enabled = false;

        baseScale = transform.localScale;

        // get each face
        for (int i = 0; i < triangles.Length; i += 3)
        {
            // TODO: inherit speed, spin...?
            Vector3 averageNormal = (normals[triangles[i]] + normals[triangles[i + 1]] + normals[triangles[i + 2]]).normalized;
            Vector3 s = GetComponent<Renderer>().bounds.size;
            float extrudeSize = ((s.x + s.y + s.z) / 3) * 0.3f;
            CreateMeshPiece(extrudeSize, transform.position, GetComponent<Renderer>().material, index, averageNormal, vertices[triangles[i]], vertices[triangles[i + 1]], vertices[triangles[i + 2]], uvs[triangles[i]], uvs[triangles[i + 1]], uvs[triangles[i + 2]]);
            if (index % 100 == 0)
                yield return new WaitForEndOfFrame();
            if (index > triangles.Length / 25)
                i = triangles.Length;
            index++;
        }
        // destroy original
        Destroy(gameObject);
    }

    void CreateMeshPiece(float extrudeSize, Vector3 pos, Material mat, int index, Vector3 faceNormal, Vector3 v1, Vector3 v2, Vector3 v3, Vector2 uv1, Vector2 uv2, Vector2 uv3)
    {
        GameObject go = new GameObject();
        go.name = "piece_" + index;
        go.transform.localScale = baseScale*1.8f;

        Mesh mesh = go.AddComponent<MeshFilter>().mesh;

        go.AddComponent<MeshRenderer>();
        go.GetComponent<Renderer>().material = mat;
        go.transform.position = pos;

        Destroy(go, 0.5f);

        Vector3[] vertices = new Vector3[3 * 4];
        int[] triangles = new int[3 * 4];
        Vector2[] uvs = new Vector2[3 * 4];

        // get centroid
        Vector3 v4 = (v1 + v2 + v3) / 3;
        // extend to backwards
        v4 = v4 + (-faceNormal) * extrudeSize;

        // not shared vertices
        // orig face
        //vertices[0] = (v1);
        vertices[0] = (v1);
        vertices[1] = (v2);
        vertices[2] = (v3);
        // right face
        vertices[3] = (v1);
        vertices[4] = (v2);
        vertices[5] = (v4);
        // left face
        vertices[6] = (v1);
        vertices[7] = (v3);
        vertices[8] = (v4);
        // bottom face
        vertices[9] = (v2);
        vertices[10] = (v3);
        vertices[11] = (v4);

        // orig face
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        //  right face
        triangles[3] = 5;
        triangles[4] = 4;
        triangles[5] = 3;
        //  left face
        triangles[6] = 6;
        triangles[7] = 7;
        triangles[8] = 8;
        //  bottom face
        triangles[9] = 11;
        triangles[10] = 10;
        triangles[11] = 9;

        // orig face
        uvs[0] = uv1;
        uvs[1] = uv2;
        uvs[2] = uv3; // todo
                      // right face
        uvs[3] = uv1;
        uvs[4] = uv2;
        uvs[5] = uv3; // todo

        // left face
        uvs[6] = uv1;
        uvs[7] = uv3;
        uvs[8] = uv3;   // todo
                        // bottom face (mirror?) or custom color? or fixed from uv?
        uvs[9] = uv1;
        uvs[10] = uv2;
        uvs[11] = uv1; // todo

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        CalculateMeshTangents(mesh);

        go.AddComponent<Rigidbody>();
        go.GetComponent<Rigidbody>().mass = 1f;
        go.GetComponent<Rigidbody>().AddExplosionForce(-10f, transform.position,0.1f,1,ForceMode.Impulse);
        MeshCollider mc = go.AddComponent<MeshCollider>();

        mc.sharedMesh = mesh;
        mc.convex = true;

        // go.AddComponent<MeshFader>();
    }

    // source: http://answers.unity3d.com/questions/7789/calculating-tangents-vector4.html
    void CalculateMeshTangents(Mesh mesh)
    {
        //speed up math by copying the mesh arrays
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uv = mesh.uv;
        Vector3[] normals = mesh.normals;

        //variable definitions
        int triangleCount = triangles.Length;
        int vertexCount = vertices.Length;

        Vector3[] tan1 = new Vector3[vertexCount];
        Vector3[] tan2 = new Vector3[vertexCount];

        Vector4[] tangents = new Vector4[vertexCount];

        for (long a = 0; a < triangleCount; a += 3)
        {
            long i1 = triangles[a + 0];
            long i2 = triangles[a + 1];
            long i3 = triangles[a + 2];

            Vector3 v1 = vertices[i1];
            Vector3 v2 = vertices[i2];
            Vector3 v3 = vertices[i3];

            Vector2 w1 = uv[i1];
            Vector2 w2 = uv[i2];
            Vector2 w3 = uv[i3];

            float x1 = v2.x - v1.x;
            float x2 = v3.x - v1.x;
            float y1 = v2.y - v1.y;
            float y2 = v3.y - v1.y;
            float z1 = v2.z - v1.z;
            float z2 = v3.z - v1.z;

            float s1 = w2.x - w1.x;
            float s2 = w3.x - w1.x;
            float t1 = w2.y - w1.y;
            float t2 = w3.y - w1.y;

            float r = 1.0f / (s1 * t2 - s2 * t1);

            Vector3 sdir = new Vector3((t2 * x1 - t1 * x2) * r, (t2 * y1 - t1 * y2) * r, (t2 * z1 - t1 * z2) * r);
            Vector3 tdir = new Vector3((s1 * x2 - s2 * x1) * r, (s1 * y2 - s2 * y1) * r, (s1 * z2 - s2 * z1) * r);

            tan1[i1] += sdir;
            tan1[i2] += sdir;
            tan1[i3] += sdir;

            tan2[i1] += tdir;
            tan2[i2] += tdir;
            tan2[i3] += tdir;
        }

        for (int a = 0; a < vertexCount; ++a)
        {
            Vector3 n = normals[a];
            Vector3 t = tan1[a];
            Vector3.OrthoNormalize(ref n, ref t);
            tangents[a].x = t.x;
            tangents[a].y = t.y;
            tangents[a].z = t.z;
            tangents[a].w = (Vector3.Dot(Vector3.Cross(n, t), tan2[a]) < 0.0f) ? -1.0f : 1.0f;
        }
        mesh.tangents = tangents;
    }
}
