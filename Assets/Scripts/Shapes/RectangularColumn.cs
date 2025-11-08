using UnityEngine;

public class RectangularColumn : MonoBehaviour
{
    private Material mat;
    private float size;
    private Vector2 pos;
    private float zpos;
    
    public void DrawRectangularColumn(Material material, float shapeSize, Vector2 shapePos, float zPos)
    {
        mat = material;
        size = shapeSize;
        pos = shapePos;
        zpos = zPos + 5f;
        
        if (mat == null)
        {
            Debug.LogError("Missing material");
            return;
        }
        GL.PushMatrix();

        GL.Begin(GL.LINES);
        mat.SetPass(0);

        var front = GetRectangularColumn();
        var frontZ = PerspectiveCamera.Instance.GetPerspective(zpos + size * .5f);
        var back = GetRectangularColumn();
        var backZ = PerspectiveCamera.Instance.GetPerspective(zpos - size * .5f);

        var computedFront = RenderRectangularColumn(front, frontZ);
        var computedBack = RenderRectangularColumn(back, backZ);

        for (int i = 0; i < 4; i++)
        {
            GL.Vertex(computedFront[i]);
            GL.Vertex(computedBack[i]);
        }

        GL.End();
        GL.PopMatrix();
    }

    private Vector2[] GetRectangularColumn()
    {
        var faceArray = new Vector2[]
        {
            new Vector2 (0.5f, 2f),
            new Vector2 (-0.5f, 2f),
            new Vector2 (-0.5f, -2f),
            new Vector2 (0.5f, -2f),
        };

        for(var i = 0; i < faceArray.Length; i++)
        {
            faceArray[i] = new Vector2(pos.x + faceArray[i].x, pos.y + faceArray[i].y) * size;
        }

        return faceArray;
        
    }

    private Vector2[] RenderRectangularColumn(Vector2[] elements, float perspective)
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
