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
public static Rhino.Commands.Result ZoomToObject(Rhino.RhinoDoc doc)
{
  Rhino.DocObjects.ObjRef rhObject;
  var rc = Rhino.Input.RhinoGet.GetOneObject("Select object to zoom", false, Rhino.DocObjects.ObjectType.None, out rhObject);
  if (rc != Rhino.Commands.Result.Success)
    return rc;

  var obj = rhObject.Object();
  var view = doc.Views.ActiveView;
  if (obj == null || view == null)
    return Rhino.Commands.Result.Failure;

  var bbox = obj.Geometry.GetBoundingBox(true);

  const double pad = 0.05;    // A little padding...
  double dx = (bbox.Max.X - bbox.Min.X) * pad;
  double dy = (bbox.Max.Y - bbox.Min.Y) * pad;
  double dz = (bbox.Max.Z - bbox.Min.Z) * pad;
  bbox.Inflate(dx, dy, dz);
  view.ActiveViewport.ZoomBoundingBox(bbox);
  view.Redraw();
  return Rhino.Commands.Result.Success;
}
}
