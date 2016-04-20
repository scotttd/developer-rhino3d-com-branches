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
public static Rhino.Commands.Result ArcLengthPoint(Rhino.RhinoDoc doc)
{
  Rhino.DocObjects.ObjRef objref;
  Rhino.Commands.Result rc = Rhino.Input.RhinoGet.GetOneObject("Select curve",
    true, Rhino.DocObjects.ObjectType.Curve,out objref);
  if(rc!= Rhino.Commands.Result.Success)
    return rc;
  Rhino.Geometry.Curve crv = objref.Curve();
  if( crv==null )
    return Rhino.Commands.Result.Failure;
 
  double crv_length = crv.GetLength();
  double length = 0;
  rc = Rhino.Input.RhinoGet.GetNumber("Length from start", true, ref length, 0, crv_length);
  if(rc!= Rhino.Commands.Result.Success)
    return rc;
 
  Rhino.Geometry.Point3d pt = crv.PointAtLength(length);
  if (pt.IsValid)
  {
    doc.Objects.AddPoint(pt);
    doc.Views.Redraw();
  }
  return Rhino.Commands.Result.Success;
}
}