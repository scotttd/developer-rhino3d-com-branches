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
public static Rhino.Commands.Result DetermineObjectLayer(Rhino.RhinoDoc doc)
{
  Rhino.DocObjects.ObjRef obref;
  Rhino.Commands.Result rc = Rhino.Input.RhinoGet.GetOneObject("Select object", true, Rhino.DocObjects.ObjectType.AnyObject, out obref);
  if (rc != Rhino.Commands.Result.Success)
    return rc;
  Rhino.DocObjects.RhinoObject rhobj = obref.Object();
  if (rhobj == null)
    return Rhino.Commands.Result.Failure;
  int index = rhobj.Attributes.LayerIndex;
  string name = doc.Layers[index].Name;
  Rhino.RhinoApp.WriteLine("The selected object's layer is '{0}'", name);
  return Rhino.Commands.Result.Success;
}
}
