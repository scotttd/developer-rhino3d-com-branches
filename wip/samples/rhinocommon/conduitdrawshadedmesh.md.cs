using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Rhino;
using Rhino.Collections;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Display;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using Rhino.Geometry.Intersect;
using Rhino.Geometry.Morphs;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.UI;
using Rhino.UI.Gumball;
using System.Runtime.InteropServices;

class TestCompileWrapperClass
{
public Rhino.Geometry.Mesh Mesh { get; set; }

protected override void CalculateBoundingBox(Rhino.Display.CalculateBoundingBoxEventArgs e)
{
  if (null != Mesh)
  {
    Rhino.Geometry.BoundingBox bbox = Mesh.GetBoundingBox(false);
    // Unites a bounding box with the current display bounding box in
    // order to ensure dynamic objects in "box" are drawn.
    e.IncludeBoundingBox(bbox);
  }
}

protected override void PostDrawObjects(Rhino.Display.DrawEventArgs e)
{
  if (null != Mesh)
  {
    Rhino.Display.DisplayMaterial material = new Rhino.Display.DisplayMaterial();
    material.Diffuse = System.Drawing.Color.Blue;
    e.Display.DrawMeshShaded(Mesh, material);
  }
}
}
