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
public class CurveSurfaceIntersectCommand : Command
{
  public override string EnglishName { get { return "csCurveSurfaceIntersect"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var gs = new GetObject();
    gs.SetCommandPrompt("select brep");
    gs.GeometryFilter = ObjectType.Brep;
    gs.DisablePreSelect();
    gs.SubObjectSelect = false;
    gs.Get();
    if (gs.CommandResult() != Result.Success)
      return gs.CommandResult();
    var brep = gs.Object(0).Brep();

    var gc = new GetObject();
    gc.SetCommandPrompt("select curve");
    gc.GeometryFilter = ObjectType.Curve;
    gc.DisablePreSelect();
    gc.SubObjectSelect = false;
    gc.Get();
    if (gc.CommandResult() != Result.Success)
      return gc.CommandResult();
    var curve = gc.Object(0).Curve();

    if (brep == null || curve == null)
      return Result.Failure;

    var tolerance = doc.ModelAbsoluteTolerance;

    Point3d[] intersection_points;
    Curve[] overlap_curves;
    if (!Intersection.CurveBrep(curve, brep, tolerance, out overlap_curves, out intersection_points))
    {
      RhinoApp.WriteLine("curve brep intersection failed");
      return Result.Nothing;
    }

    foreach (var overlap_curve in overlap_curves)
      doc.Objects.AddCurve(overlap_curve);
    foreach (var intersection_point in intersection_points)
      doc.Objects.AddPoint(intersection_point);

    RhinoApp.WriteLine("{0} overlap curves, and {1} intersection points", overlap_curves.Length, intersection_points.Length);
    doc.Views.Redraw();

    return Result.Success;
  }
}
}
