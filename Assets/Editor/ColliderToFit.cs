using UnityEngine;
using UnityEditor;
using System.Collections;

public class ColliderToFit : MonoBehaviour
{
    [MenuItem("GamayTools/Fit Selected Collider to Childrens")]
    static void FitToChildren()
    {
        foreach (GameObject rootGameObject in Selection.gameObjects)
        {
            if (!(rootGameObject.GetComponent<Collider2D>() is BoxCollider2D))
            {
                print("No collider into game object");
                continue;
            }
                

            bool hasBounds = false;
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

            for (int i = 0; i < rootGameObject.transform.childCount; ++i)
            {
                Renderer childRenderer = rootGameObject.transform.GetChild(i).Find("Skin").GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    if (hasBounds)
                    {
                        bounds.Encapsulate(childRenderer.bounds);
                    }
                    else
                    {
                        bounds = childRenderer.bounds;
                        hasBounds = true;
                    }
                }
            }

            BoxCollider2D collider = (BoxCollider2D)rootGameObject.GetComponent<Collider2D>();
            collider.offset = bounds.center - rootGameObject.transform.position;
            collider.size = bounds.size;
            print("working");
        }
    }

}