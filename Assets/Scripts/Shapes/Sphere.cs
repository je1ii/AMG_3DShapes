using UnityEngine;

public class Sphere : MonoBehaviour
{
    public void DrawSphere(Material material, float zPos, float radius)
    {
        if (material == null)
        {
            Debug.LogError("Missing material");
            return;
        }

        GL.PushMatrix();
        GL.Begin(GL.LINES);
        material.SetPass(0);

        var latitudeSegments = 16;   // Horizontal rings
        var longitudeSegments = 16;  // Vertical rings

        // Loop through latitude (top to bottom)
        for (var lat = 0; lat <= latitudeSegments; lat++)
        {
            var theta = Mathf.PI * lat / latitudeSegments; // 0 (north pole) → PI (south pole)
            var sinTheta = Mathf.Sin(theta);
            var cosTheta = Mathf.Cos(theta);

            // Loop through longitude (around the sphere)
            for (var lon = 0; lon <= longitudeSegments; lon++)
            {
                var phi = 2 * Mathf.PI * lon / longitudeSegments; // 0 → 2PI (full circle)

                // Convert spherical coordinates to 3D points
                var x = radius * sinTheta * Mathf.Cos(phi);
                var y = radius * cosTheta;
                var z = radius * sinTheta * Mathf.Sin(phi);

                // Apply perspective scaling based on z depth
                var perspective = PerspectiveCamera.Instance.GetPerspective(zPos + z);
                var projected = new Vector3(x * perspective, y * perspective, 0);

                // Connect longitude lines (horizontal connections)
                if (lon < longitudeSegments)
                {
                    var nextPhi = 2 * Mathf.PI * (lon + 1) / longitudeSegments;
                    var nx = radius * sinTheta * Mathf.Cos(nextPhi);
                    var nz = radius * sinTheta * Mathf.Sin(nextPhi);
                    var nperspective = PerspectiveCamera.Instance.GetPerspective(zPos + nz);
                    Vector3 nextProjected = new Vector3(nx * nperspective, y * nperspective, 0);

                    GL.Vertex(projected);
                    GL.Vertex(nextProjected);
                }

                // Connect latitude lines (vertical connections)
                if (lat < latitudeSegments)
                {
                    var nextTheta = Mathf.PI * (lat + 1) / latitudeSegments;
                    var nSinTheta = Mathf.Sin(nextTheta);
                    var nCosTheta = Mathf.Cos(nextTheta);
                    var ny = radius * nCosTheta;
                    var nx2 = radius * nSinTheta * Mathf.Cos(phi);
                    var nz2 = radius * nSinTheta * Mathf.Sin(phi);
                    var nperspective2 = PerspectiveCamera.Instance.GetPerspective(zPos + nz2);
                    var nextLatProj = new Vector3(nx2 * nperspective2, ny * nperspective2, 0);

                    GL.Vertex(projected);
                    GL.Vertex(nextLatProj);
                }
            }
        }

        GL.End();
        GL.PopMatrix();
    }
}
