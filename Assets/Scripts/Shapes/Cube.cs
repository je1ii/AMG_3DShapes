using UnityEngine;

public class Cube : MonoBehaviour
{
    private Material mat;
    private float size;
    private Vector2 pos;
    private float zpos;
    
    public void DrawCube(Material material, float shapeSize, Vector2 shapePos, float zPos)
    {
        mat = material;
        size = shapeSize;
        pos = shapePos;
        zpos = zPos;
        
        if (mat == null)
        {
            Debug.LogError("Missing material");
            return;
        }
        GL.PushMatrix();

        GL.Begin(GL.LINES);
        mat.SetPass(0);

        var frontSquare = GetCube();
        var frontZ = PerspectiveCamera.Instance.GetPerspective(zpos + size * .5f);
        var backSquare = GetCube();
        var backZ = PerspectiveCamera.Instance.GetPerspective(zpos - size * .5f);

        var computedFront = RenderSquare(frontSquare, frontZ);
        var computedBack = RenderSquare(backSquare, backZ);

        for (int i = 0; i < 4; i++)
        {
            GL.Vertex(computedFront[i]);
            GL.Vertex(computedBack[i]);
        }

        GL.End();
        GL.PopMatrix();
    }

    private Vector2[] GetCube()
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

    private Vector2[] RenderSquare(Vector2[] squareElements, float perspective)
    {
        var computedSquare = new Vector2[squareElements.Length];
        for(var i = 0; i < squareElements.Length; i++)
        {
            computedSquare[i] = squareElements[i] * perspective;
            GL.Vertex(squareElements[i] * perspective);
            GL.Vertex(squareElements[(i + 1) % squareElements.Length] * perspective);
        }
        return computedSquare;
    }
}
