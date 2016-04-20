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
public class AddRadialDimensionCommand : Rhino.Commands.Command
{
  public override string EnglishName
  {
    get { return "csAddRadialDimension"; }
  }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    ObjRef obj_ref;
    var rc = RhinoGet.GetOneObject("Select curve for radius dimension", 
      true, ObjectType.Curve, out obj_ref);
    if (rc != Result.Success)
      return rc;
    double curve_parameter;
    var curve = obj_ref.CurveParameter(out curve_parameter);
    if (curve == null)
      return Result.Failure;

    if (curve.IsLinear() || curve.IsPolyline())
    {
      RhinoApp.WriteLine("Curve must be non-linear.");
      return Result.Nothing;
    }

    // in this example just deal with planar curves
    if (!curve.IsPlanar())
    {
      RhinoApp.WriteLine("Curve must be planar.");
      return Result.Nothing;
    }

    var point_on_curve = curve.PointAt(curve_parameter);
    var curvature_vector = curve.CurvatureAt(curve_parameter);
    var len = curvature_vector.Length;
    if (len < RhinoMath.SqrtEpsilon)
    {
      RhinoApp.WriteLine("Curve is almost linear and therefore has no curvature.");
      return Result.Nothing;
    }

    var center = point_on_curve + (curvature_vector/(len*len));
    Plane plane;
    curve.TryGetPlane(out plane);
    var radial_dimension = 
      new RadialDimension(center, point_on_curve, plane.XAxis, plane.Normal, 5.0);
    doc.Objects.AddRadialDimension(radial_dimension);
    doc.Views.Redraw();
    return Result.Success;
  }
}
}