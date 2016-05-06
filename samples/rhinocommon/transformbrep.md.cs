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
public static Rhino.Commands.Result TransformBrep(Rhino.RhinoDoc doc)
{
  Rhino.DocObjects.ObjRef rhobj;
  var rc = RhinoGet.GetOneObject("Select brep", true, Rhino.DocObjects.ObjectType.Brep, out rhobj);
  if(rc!= Rhino.Commands.Result.Success)
    return rc;

  // Simple translation transformation
  var xform = Rhino.Geometry.Transform.Translation(18,-18,25);
  doc.Objects.Transform(rhobj, xform, true);
  doc.Views.Redraw();
  return Rhino.Commands.Result.Success;
}
}
