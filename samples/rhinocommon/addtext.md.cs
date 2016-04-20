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
public static Rhino.Commands.Result AddAnnotationText(Rhino.RhinoDoc doc)
{
  Rhino.Geometry.Point3d pt = new Rhino.Geometry.Point3d(10, 0, 0);
  const string text = "Hello RhinoCommon";
  const double height = 2.0;
  const string font = "Arial";
  Rhino.Geometry.Plane plane = doc.Views.ActiveView.ActiveViewport.ConstructionPlane();
  plane.Origin = pt;
  Guid id = doc.Objects.AddText(text, plane, height, font, false, false);
  if( id != Guid.Empty )
  {
    doc.Views.Redraw();
    return Rhino.Commands.Result.Success;
  }
  return Rhino.Commands.Result.Failure;
}
}
