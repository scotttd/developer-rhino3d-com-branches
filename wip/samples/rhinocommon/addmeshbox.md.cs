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
public static Rhino.Commands.Result AddMeshBox(Rhino.RhinoDoc doc)
{
  Rhino.Geometry.Box box;
  Rhino.Commands.Result rc = Rhino.Input.RhinoGet.GetBox(out box);
  if (rc == Rhino.Commands.Result.Success)
  {
    Rhino.Geometry.Mesh mesh = Rhino.Geometry.Mesh.CreateFromBox(box, 2, 2, 2);
    if (null != mesh)
    {
      doc.Objects.AddMesh(mesh);
      doc.Views.Redraw();
      return Rhino.Commands.Result.Success;
    }
  }

  return Rhino.Commands.Result.Failure;
}
}
