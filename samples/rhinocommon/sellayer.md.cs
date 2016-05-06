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
public static Rhino.Commands.Result SelLayer(Rhino.RhinoDoc doc)
{
  // Prompt for a layer name
  string layername = doc.Layers.CurrentLayer.Name;
  Result rc = Rhino.Input.RhinoGet.GetString("Name of layer to select objects", true, ref layername);
  if (rc != Rhino.Commands.Result.Success)
    return rc;

  // Get all of the objects on the layer. If layername is bogus, you will
  // just get an empty list back
  Rhino.DocObjects.RhinoObject[] rhobjs = doc.Objects.FindByLayer(layername);
  if (rhobjs == null || rhobjs.Length < 1)
    return Rhino.Commands.Result.Cancel;

  for (int i = 0; i < rhobjs.Length; i++)
    rhobjs[i].Select(true);
  doc.Views.Redraw();
  return Rhino.Commands.Result.Success;
}
}
