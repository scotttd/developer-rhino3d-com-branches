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
public static Rhino.Commands.Result BlockInsertionPoint(Rhino.RhinoDoc doc)
{
  Rhino.DocObjects.ObjRef objref;
  Result rc = Rhino.Input.RhinoGet.GetOneObject("Select instance", true, Rhino.DocObjects.ObjectType.InstanceReference, out objref);
  if (rc != Rhino.Commands.Result.Success)
    return rc;
  Rhino.DocObjects.InstanceObject instance = objref.Object() as Rhino.DocObjects.InstanceObject;
  if (instance != null)
  {
    Rhino.Geometry.Point3d pt = instance.InsertionPoint;
    doc.Objects.AddPoint(pt);
    doc.Views.Redraw();
  }
  return Rhino.Commands.Result.Success;
}
}
