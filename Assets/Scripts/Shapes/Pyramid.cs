using UnityEngine;

public class Pyramid : MonoBehaviour
{
    private Material mat;
    private float size;
    private Vector2 pos;
    private float zpos;
    
    public void DrawPyramid(Material material, float shapeSize, Vector2 shapePos, float zPos)
    {
        mat = material;
        size = shapeSize;
        pos = shapePos;
        zpos = zPos;
        
        if (mat == null)
        {
            Debug.LogError("You need to add a material");
            return;
        }
        GL.PushMatrix();

        GL.Begin(GL.LINES);
        mat.SetPass(0);

        var front = new Vector2(0, 2f);
        var back = GetPyramid();
        var backZ = PerspectiveCamera.Instance.GetPerspective(zpos - size * .5f);

        var computedFront = front;
        var computedBack = RenderPyramid(back, backZ);

        for (int i = 0; i < 4; i++)
        {
            GL.Vertex(computedFront);
            GL.Vertex(computedBack[i]);
        }

        GL.End();
        GL.PopMatrix();
    }

    private Vector2[] GetPyramid()
    {
        var faceArray = new Vector2[]
        {
            new Vector2 (1, 1f),
            new Vector2 (-1f, 1f),
            new Vector2 (-1f, -1f),
            new Vector2 (1f, -1f),
        };

        for(var i = 0; i < faceArray.Length; i++)
        {
            faceArray[i] = new Vector2(pos.x + faceArray[i].x, pos.y + faceArray[i].y) * size;
        }

        return faceArray;
        
    }

    private Vector2[] RenderPyramid(Vector2[] elements, float perspective)
    {
        var computedShape = new Vector2[elements.Length];
        for(var i = 0; i < elements.Length; i++)
        {
            computedShape[i] = elements[i] * perspective;
            GL.Vertex(elements[i] * perspective);
            GL.Vertex(elements[(i + 1) % elements.Length] * perspective);
        }
        return computedShape;
    }
}
