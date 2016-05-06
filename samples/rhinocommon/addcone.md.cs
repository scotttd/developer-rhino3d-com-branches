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
public static Rhino.Commands.Result AddCone(Rhino.RhinoDoc doc)
{
  Rhino.Geometry.Plane plane = Rhino.Geometry.Plane.WorldXY;
  const double height = 10;
  const double radius = 5;
  Rhino.Geometry.Cone cone = new Rhino.Geometry.Cone(plane, height, radius);
  if (cone.IsValid)
  {
    const bool cap_bottom = true;
    Rhino.Geometry.Brep cone_brep = cone.ToBrep(cap_bottom);
    if (cone_brep!=null)
    {
      doc.Objects.AddBrep(cone_brep);
      doc.Views.Redraw();
    }
  }
  return Rhino.Commands.Result.Success;
}
}
