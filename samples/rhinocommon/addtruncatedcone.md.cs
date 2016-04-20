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
public static Rhino.Commands.Result AddTruncatedCone(Rhino.RhinoDoc doc)
{
  Point3d bottom_pt = new Point3d(0,0,0);
  const double bottom_radius = 2;
  Circle bottom_circle = new Circle(bottom_pt, bottom_radius);

  Point3d top_pt = new Point3d(0,0,10);
  const double top_radius = 6;
  Circle top_circle = new Circle(top_pt, top_radius);

  LineCurve shapeCurve = new LineCurve(bottom_circle.PointAt(0), top_circle.PointAt(0));
  Line axis = new Line(bottom_circle.Center, top_circle.Center);
  RevSurface revsrf = RevSurface.Create(shapeCurve, axis);
  Brep tcone_brep = Brep.CreateFromRevSurface(revsrf, true, true);
  if( doc.Objects.AddBrep(tcone_brep) != Guid.Empty )
  {
    doc.Views.Redraw();
    return Rhino.Commands.Result.Success;
  }
  return Rhino.Commands.Result.Failure;
}
}
