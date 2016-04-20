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
public override string EnglishName { get { return "cs_analysismode_on"; } }

protected override Rhino.Commands.Result RunCommand(RhinoDoc doc, Rhino.Commands.RunMode mode)
{
  // make sure our custom visual analysis mode is registered
  var zmode = Rhino.Display.VisualAnalysisMode.Register(typeof(ZAnalysisMode));

  const ObjectType filter = Rhino.DocObjects.ObjectType.Surface | Rhino.DocObjects.ObjectType.PolysrfFilter | Rhino.DocObjects.ObjectType.Mesh;
  Rhino.DocObjects.ObjRef[] objs;
  var rc = Rhino.Input.RhinoGet.GetMultipleObjects("Select objects for Z analysis", false, filter, out objs);
  if (rc != Rhino.Commands.Result.Success)
    return rc;

  int count = 0;
  for (int i = 0; i < objs.Length; i++)
  {
    var obj = objs[i].Object();

    // see if this object is alreay in Z analysis mode
    if (obj.InVisualAnalysisMode(zmode))
      continue;

    if (obj.EnableVisualAnalysisMode(zmode, true))
      count++;
  }
  doc.Views.Redraw();
  RhinoApp.WriteLine("{0} objects were put into Z-Analysis mode.", count);
  return Rhino.Commands.Result.Success;
}
}
