using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{
    public Mesh mesh;
    public Material material;

    public int maxDepth;

    private int depth;

    private void Start()
    {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        gameObject.GetComponent<Transform>().localScale = new Vector3(2.4f, 1.2f, 0.6f);


        if (depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }
    }

    private static Vector3[] childDirections =
    {
        Vector3.up,
        Vector3.down,
        //Vector3.right,
        //Vector3.left,
        Vector3.forward,
        Vector3.back,
    };

    private static Quaternion[] childOrientations =
    {
        //Quaternion.identity,
        
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(-90f,0f, 0f),
        Quaternion.Euler( 90f,0f, 0f),
        Quaternion.Euler(0f, -90f, 0f),
        Quaternion.Euler( 0f,90f, 0f),


    };


    private IEnumerator CreateChildren()
    {

        for (int i = 0; i < childDirections.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            new GameObject("Fractal Child").AddComponent<Fractal>()
                .Initialize(this, i);
            //Destroy(this, 1.0f);
        }

        //yield return new WaitForSeconds(0.5f);
        //new GameObject("Fractal Child").AddComponent<Fractal>()
        //    .Initialize(this, Vector3.right, Quaternion.Euler(0f, 0f, -90f));

        //yield return new WaitForSeconds(0.5f);
        //new GameObject("Fractal Child").AddComponent<Fractal>()
        //    .Initialize(this, Vector3.left, Quaternion.Euler(0f, 0f, 90f));
    }

    public float childScale;

    private void Initialize(Fractal parent, int childIndex)
    {
        this.mesh = parent.mesh;
        this.material = parent.material;
        this.maxDepth = parent.maxDepth;
        this.depth = parent.depth + 1;
        this.childScale = parent.childScale;
        this.transform.parent = parent.transform;
        this.transform.localScale = Vector3.one * childScale;

        this.transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
        this.transform.localRotation = childOrientations[childIndex];
    }

}
