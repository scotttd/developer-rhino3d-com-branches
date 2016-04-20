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
public static Rhino.Commands.Result DupBorder(Rhino.RhinoDoc doc)
{
  const ObjectType filter = Rhino.DocObjects.ObjectType.Surface | Rhino.DocObjects.ObjectType.PolysrfFilter;
  Rhino.DocObjects.ObjRef objref;
  Rhino.Commands.Result rc = Rhino.Input.RhinoGet.GetOneObject("Select surface or polysurface", false, filter, out objref);
  if (rc != Rhino.Commands.Result.Success || objref == null)
    return rc;

  Rhino.DocObjects.RhinoObject rhobj = objref.Object();
  Rhino.Geometry.Brep brep = objref.Brep();
  if (rhobj == null || brep == null)
    return Rhino.Commands.Result.Failure;

  rhobj.Select(false);
  Rhino.Geometry.Curve[] curves = brep.DuplicateEdgeCurves(true);
  double tol = doc.ModelAbsoluteTolerance * 2.1;
  curves = Rhino.Geometry.Curve.JoinCurves(curves, tol);
  for (int i = 0; i < curves.Length; i++)
  {
    Guid id = doc.Objects.AddCurve(curves[i]);
    doc.Objects.Select(id);
  }
  doc.Views.Redraw();
  return Rhino.Commands.Result.Success;
}
}
