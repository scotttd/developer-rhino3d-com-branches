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
public static Rhino.Commands.Result AddLine(Rhino.RhinoDoc doc)
{
  Rhino.Input.Custom.GetPoint gp = new Rhino.Input.Custom.GetPoint();
  gp.SetCommandPrompt("Start of line");
  gp.Get();
  if (gp.CommandResult() != Rhino.Commands.Result.Success)
    return gp.CommandResult();

  Rhino.Geometry.Point3d pt_start = gp.Point();

  gp.SetCommandPrompt("End of line");
  gp.SetBasePoint(pt_start, false);
  gp.DrawLineFromPoint(pt_start, true);
  gp.Get();
  if (gp.CommandResult() != Rhino.Commands.Result.Success)
    return gp.CommandResult();

  Rhino.Geometry.Point3d pt_end = gp.Point();
  Rhino.Geometry.Vector3d v = pt_end - pt_start;
  if (v.IsTiny(Rhino.RhinoMath.ZeroTolerance))
    return Rhino.Commands.Result.Nothing;

  if (doc.Objects.AddLine(pt_start, pt_end) != Guid.Empty)
  {
    doc.Views.Redraw();
    return Rhino.Commands.Result.Success;
  }
  return Rhino.Commands.Result.Failure;
}
}
