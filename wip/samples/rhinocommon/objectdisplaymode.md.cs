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
public static Rhino.Commands.Result ObjectDisplayMode(Rhino.RhinoDoc doc)
{
  const ObjectType filter = ObjectType.Mesh | ObjectType.Brep;
  ObjRef objref;
  Result rc = Rhino.Input.RhinoGet.GetOneObject("Select mesh or surface", true, filter, out objref);
  if (rc != Rhino.Commands.Result.Success)
    return rc;
  Guid viewportId = doc.Views.ActiveView.ActiveViewportID;

  ObjectAttributes attr = objref.Object().Attributes;
  if (attr.HasDisplayModeOverride(viewportId))
  {
    RhinoApp.WriteLine("Removing display mode override from object");
    attr.RemoveDisplayModeOverride(viewportId);
  }
  else
  {
    Rhino.Display.DisplayModeDescription[] modes = Rhino.Display.DisplayModeDescription.GetDisplayModes();
    Rhino.Display.DisplayModeDescription mode = null;
    if (modes.Length == 1)
      mode = modes[0];
    else
    {
      Rhino.Input.Custom.GetOption go = new Rhino.Input.Custom.GetOption();
      go.SetCommandPrompt("Select display mode");
      string[] str_modes = new string[modes.Length];
      for (int i = 0; i < modes.Length; i++)
        str_modes[i] = modes[i].EnglishName.Replace(" ", "").Replace("-", "");
      go.AddOptionList("DisplayMode", str_modes, 0);
      if (go.Get() == Rhino.Input.GetResult.Option)
        mode = modes[go.Option().CurrentListOptionIndex];
    }
    if (mode == null)
      return Rhino.Commands.Result.Cancel;
    attr.SetDisplayModeOverride(mode, viewportId);
  }
  doc.Objects.ModifyAttributes(objref, attr, false);
  doc.Views.Redraw();
  return Rhino.Commands.Result.Success;
}
}
