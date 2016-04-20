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
public static Rhino.Commands.Result TweenCurve(Rhino.RhinoDoc doc)
{
  Rhino.Input.Custom.GetObject go = new Rhino.Input.Custom.GetObject();
  go.SetCommandPrompt("Select two curves");
  go.GeometryFilter = Rhino.DocObjects.ObjectType.Curve;
  go.GetMultiple(2, 2);
  if (go.CommandResult() != Rhino.Commands.Result.Success)
    return go.CommandResult();

  Rhino.Geometry.Curve curve0 = go.Object(0).Curve();
  Rhino.Geometry.Curve curve1 = go.Object(1).Curve();
  if (null != curve0 && null != curve1)
  {
    Rhino.Geometry.Curve[] curves = Rhino.Geometry.Curve.CreateTweenCurves(curve0, curve1, 1);
    if (null != curves)
    {
      for (int i = 0; i < curves.Length; i++)
        doc.Objects.AddCurve(curves[i]);

      doc.Views.Redraw();
      return Rhino.Commands.Result.Success;
    }
  }

  return Rhino.Commands.Result.Failure;
}
}
