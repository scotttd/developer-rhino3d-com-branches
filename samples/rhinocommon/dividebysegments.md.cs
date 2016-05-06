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
public class DivideBySegmentsCommand : Rhino.Commands.Command
{
  public override string EnglishName
  {
    get { return "csDivideCurveBySegments"; }
  }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    const ObjectType filter = ObjectType.Curve; 
    ObjRef objref;
    var rc = RhinoGet.GetOneObject("Select curve to divide", false, filter, out objref);
    if (rc != Result.Success || objref == null)
      return rc;

    var curve = objref.Curve();
    if (curve == null || curve.IsShort(RhinoMath.ZeroTolerance))
      return Result.Failure;

    var segment_count = 2;
    rc = RhinoGet.GetInteger("Divide curve into how many segments?", false, ref segment_count);
    if (rc != Result.Success)
      return rc;

    Point3d[] points;
    curve.DivideByCount(segment_count, true, out points);
    if (points == null)
      return Result.Failure;

    foreach (var point in points)
      doc.Objects.AddPoint(point);

    doc.Views.Redraw();
    return Result.Success;
  }
}
}
