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
public static Rhino.Commands.Result LineBetweenCurves(Rhino.RhinoDoc doc)
{
  Rhino.Input.Custom.GetObject go = new Rhino.Input.Custom.GetObject();
  go.SetCommandPrompt("Select two curves");
  go.GeometryFilter = Rhino.DocObjects.ObjectType.Curve;
  go.GetMultiple(2, 2);
  if (go.CommandResult() != Rhino.Commands.Result.Success)
    return go.CommandResult();

  Rhino.DocObjects.ObjRef objRef0 = go.Object(0);
  Rhino.DocObjects.ObjRef objRef1 = go.Object(1);

  double t0 = Rhino.RhinoMath.UnsetValue;
  double t1 = Rhino.RhinoMath.UnsetValue;
  Rhino.Geometry.Curve curve0 = objRef0.CurveParameter(out t0);
  Rhino.Geometry.Curve curve1 = objRef1.CurveParameter(out t1);
  if (null == curve0 || !Rhino.RhinoMath.IsValidDouble(t0) ||
      null == curve1 || !Rhino.RhinoMath.IsValidDouble(t1) )
    return Rhino.Commands.Result.Failure;

  Rhino.Geometry.Line line = Rhino.Geometry.Line.Unset;
  bool rc = Rhino.Geometry.Line.TryCreateBetweenCurves(curve0, curve1, ref t0, ref t1, false, false, out line);
  if (rc)
  {
    if (Guid.Empty != doc.Objects.AddLine(line))
    {
      doc.Views.Redraw();
      return Rhino.Commands.Result.Success;
    }
  }
  return Rhino.Commands.Result.Failure;
}
}
