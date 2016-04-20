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
public static Rhino.Commands.Result AddNurbsCurve(Rhino.RhinoDoc doc)
{
  Rhino.Collections.Point3dList points = new Rhino.Collections.Point3dList(5);
  points.Add(0, 0, 0);
  points.Add(0, 2, 0);
  points.Add(2, 3, 0);
  points.Add(4, 2, 0);
  points.Add(4, 0, 0);
  Rhino.Geometry.NurbsCurve nc = Rhino.Geometry.NurbsCurve.Create(false, 3, points);
  Rhino.Commands.Result rc = Rhino.Commands.Result.Failure;
  if (nc != null && nc.IsValid)
  {
    if (doc.Objects.AddCurve(nc) != Guid.Empty)
    {
      doc.Views.Redraw();
      rc = Rhino.Commands.Result.Success;
    }
  }
  return rc;
}
}
