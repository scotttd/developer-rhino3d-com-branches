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
public static Rhino.Commands.Result ObjectColor(Rhino.RhinoDoc doc)
{
  Rhino.DocObjects.ObjRef[] objRefs;
  Rhino.Commands.Result cmdResult = Rhino.Input.RhinoGet.GetMultipleObjects("Select objects to change color", false, Rhino.DocObjects.ObjectType.AnyObject, out objRefs);
  if (cmdResult != Rhino.Commands.Result.Success)
    return cmdResult;

  System.Drawing.Color color = System.Drawing.Color.Black;
  bool rc = Rhino.UI.Dialogs.ShowColorDialog(ref color);
  if (!rc)
    return Rhino.Commands.Result.Cancel;

  for (int i = 0; i < objRefs.Length; i++)
  {
    Rhino.DocObjects.RhinoObject obj = objRefs[i].Object();
    if (null == obj || obj.IsReference)
      continue;

    if (color != obj.Attributes.ObjectColor)
    {
      obj.Attributes.ObjectColor = color;
      obj.Attributes.ColorSource = Rhino.DocObjects.ObjectColorSource.ColorFromObject;
      obj.CommitChanges();
    }
  }

  doc.Views.Redraw();

  return Rhino.Commands.Result.Success;
}
}
