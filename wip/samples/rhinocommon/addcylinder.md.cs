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
public static Rhino.Commands.Result AddCylinder(Rhino.RhinoDoc doc)
{
  Rhino.Geometry.Point3d center_point = new Rhino.Geometry.Point3d(0, 0, 0);
  Rhino.Geometry.Point3d height_point = new Rhino.Geometry.Point3d(0, 0, 10);
  Rhino.Geometry.Vector3d zaxis = height_point - center_point;
  Rhino.Geometry.Plane plane = new Rhino.Geometry.Plane(center_point, zaxis);
  const double radius = 5;
  Rhino.Geometry.Circle circle = new Rhino.Geometry.Circle(plane, radius);
  Rhino.Geometry.Cylinder cylinder = new Rhino.Geometry.Cylinder(circle, zaxis.Length);
  Rhino.Geometry.Brep brep = cylinder.ToBrep(true, true);
  if (brep != null)
  {
    doc.Objects.AddBrep(brep);
    doc.Views.Redraw();
  }
  return Rhino.Commands.Result.Success;
}
}
