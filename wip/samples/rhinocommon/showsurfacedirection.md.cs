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
public static Rhino.Commands.Result ShowSurfaceDirection(Rhino.RhinoDoc doc)
{
  Rhino.DocObjects.ObjRef objref;
  var rc = Rhino.Input.RhinoGet.GetOneObject("Select surface or polysurface for direction display",
    false,
    Rhino.DocObjects.ObjectType.Surface | Rhino.DocObjects.ObjectType.PolysrfFilter,
    out objref);
  if (rc != Rhino.Commands.Result.Success)
    return rc;

  var brep = objref.Brep();
  if (brep == null)
    return Rhino.Commands.Result.Failure;

  bool bIsSolid = brep.IsSolid;

  TestSurfaceDirConduit conduit = new TestSurfaceDirConduit(brep);
  conduit.Enabled = true;
  doc.Views.Redraw();

  var gf = new Rhino.Input.Custom.GetOption();
  gf.SetCommandPrompt("Press enter when done");
  gf.AcceptNothing(true);
  if (!bIsSolid)
    gf.AddOption("Flip");

  for (; ; )
  {
    var res = gf.Get();
    if (res == Rhino.Input.GetResult.Option)
    {
      conduit.Flip = !conduit.Flip;
      doc.Views.Redraw();
      continue;
    }
    if (res == Rhino.Input.GetResult.Nothing)
    {
      if (!bIsSolid && conduit.Flip)
      {
        brep.Flip();
        doc.Objects.Replace(objref, brep);
      }
    }
    break;
  }

  conduit.Enabled = false;
  doc.Views.Redraw();
  return Rhino.Commands.Result.Success;
}
}
