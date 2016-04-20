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
public class LeaderCommand : Command
{
  public override string EnglishName { get { return "csLeader"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var points = new Point3d[]
    {
      new Point3d(1, 1, 0),
      new Point3d(5, 1, 0),
      new Point3d(5, 5, 0),
      new Point3d(9, 5, 0)
    };

    var xy_plane = Plane.WorldXY;

    var points2d = new List<Point2d>();
    foreach (var point3d in points)
    {
      double x, y;
      if (xy_plane.ClosestParameter(point3d, out x, out y))
      {
        var point2d = new Point2d(x, y);
        if (points2d.Count < 1 || point2d.DistanceTo(points2d.Last<Point2d>()) > RhinoMath.SqrtEpsilon)
          points2d.Add(point2d);
      }
    }

    doc.Objects.AddLeader(xy_plane, points2d);
    doc.Views.Redraw();
    return Result.Success;
  }
}
}
