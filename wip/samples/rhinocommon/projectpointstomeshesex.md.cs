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
public class ProjectPointsToMeshesExCommand : Command
{
  public override string EnglishName { get { return "csProjectPointsToMeshesEx"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    ObjRef obj_ref;
    var rc = RhinoGet.GetOneObject("mesh", false, ObjectType.Mesh, out obj_ref);
    if (rc != Result.Success) return rc;
    var mesh = obj_ref.Mesh();

    ObjRef[] obj_ref_pts;
    rc = RhinoGet.GetMultipleObjects("points", false, ObjectType.Point, out obj_ref_pts);
    if (rc != Result.Success) return rc;
    var points = new List<Point3d>();
    foreach (var obj_ref_pt in obj_ref_pts)
    {
      var pt = obj_ref_pt.Point().Location;
      points.Add(pt);
    }

    int[] indices;
    var prj_points = Intersection.ProjectPointsToMeshesEx(new[] {mesh}, points, new Vector3d(0, 1, 0), 0, out indices);
    foreach (var prj_pt in prj_points) doc.Objects.AddPoint(prj_pt);
    doc.Views.Redraw();
    return Result.Success;
  }
}
}
