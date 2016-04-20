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
public class GetAngleCommand : Command
{
  public override string EnglishName { get { return "csGetAngle"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var gp = new GetPoint();
    gp.SetCommandPrompt("Base point");
    gp.Get();
    if (gp.CommandResult() != Result.Success)
      return gp.CommandResult();
    var base_point = gp.Point();

    gp.SetCommandPrompt("First reference point");
    gp.DrawLineFromPoint(base_point, true);
    gp.Get();
    if (gp.CommandResult() != Result.Success)
      return gp.CommandResult();
    var first_point = gp.Point();

    double angle_radians;
    var rc = RhinoGet.GetAngle("Second reference point", base_point, first_point, 0, out angle_radians);
    if (rc == Result.Success)
      RhinoApp.WriteLine("Angle = {0} degrees", RhinoMath.ToDegrees(angle_radians));

    return rc;
  }
}
}
