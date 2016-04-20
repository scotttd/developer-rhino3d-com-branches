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
public class NurbsSurfaceIncreaseDegreeCommand : Command
{
  public override string EnglishName { get { return "csNurbsSrfIncreaseDegree"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    ObjRef obj_ref;
    var rc = RhinoGet.GetOneObject(
      "Select surface", false, ObjectType.Surface, out obj_ref);
    if (rc != Result.Success) return rc;
    if (obj_ref == null) return Result.Failure;
    var surface = obj_ref.Surface();
    if (surface == null) return Result.Failure;
    var nurbs_surface = surface.ToNurbsSurface();

    int new_u_degree = -1;
    rc = RhinoGet.GetInteger(string.Format("New U degree <{0}...11>", nurbs_surface.Degree(0)), true, ref new_u_degree,
      nurbs_surface.Degree(0), 11);
    if (rc != Result.Success) return rc;
    
    int new_v_degree = -1;
    rc = RhinoGet.GetInteger(string.Format("New V degree <{0}...11>", nurbs_surface.Degree(1)), true, ref new_v_degree,
      nurbs_surface.Degree(1), 11);
    if (rc != Result.Success) return rc;

    rc = Result.Failure;
    if (nurbs_surface.IncreaseDegreeU(new_u_degree))
      if (nurbs_surface.IncreaseDegreeV(new_v_degree))
        if (doc.Objects.Replace(obj_ref.ObjectId, nurbs_surface))
          rc = Result.Success;

    RhinoApp.WriteLine("Result: {0}", rc.ToString());
    doc.Views.Redraw();
    return rc;
  }
}
}
