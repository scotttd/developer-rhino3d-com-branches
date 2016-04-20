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
public class GetPointDynamicDrawCommand : Command
{
  public override string EnglishName { get { return "csGetPointDynamicDraw"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var gp = new GetPoint();
    gp.SetCommandPrompt("Center point");
    gp.Get();
    if (gp.CommandResult() != Result.Success)
      return gp.CommandResult();
    var center_point = gp.Point();
    if (center_point == Point3d.Unset)
      return Result.Failure;

    var gcp = new GetCircleRadiusPoint(center_point);
    gcp.SetCommandPrompt("Radius");
    gcp.ConstrainToConstructionPlane(false);
    gcp.SetBasePoint(center_point, true);
    gcp.DrawLineFromPoint(center_point, true);
    gcp.Get();
    if (gcp.CommandResult() != Result.Success)
      return gcp.CommandResult();

    var radius = center_point.DistanceTo(gcp.Point());
    var cplane = doc.Views.ActiveView.ActiveViewport.ConstructionPlane();
    doc.Objects.AddCircle(new Circle(cplane, center_point, radius));
    doc.Views.Redraw();
    return Result.Success;
  }
}

public class GetCircleRadiusPoint : GetPoint
{
  private Point3d m_center_point;
 
  public GetCircleRadiusPoint(Point3d centerPoint)
  {
    m_center_point = centerPoint;
  }

  protected override void OnDynamicDraw(GetPointDrawEventArgs e)
  {
    base.OnDynamicDraw(e);
    var cplane = e.RhinoDoc.Views.ActiveView.ActiveViewport.ConstructionPlane();
    var radius = m_center_point.DistanceTo(e.CurrentPoint);
    var circle = new Circle(cplane, m_center_point, radius);
    e.Display.DrawCircle(circle, System.Drawing.Color.Black);
  }
}
}
