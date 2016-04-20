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
public static Rhino.Commands.Result ActiveViewport(Rhino.RhinoDoc doc)
{
  Rhino.Display.RhinoView view = doc.Views.ActiveView;
  if (view == null)
    return Rhino.Commands.Result.Failure;

  Rhino.Display.RhinoPageView pageview = view as Rhino.Display.RhinoPageView;
  if (pageview != null)
  {
    string layout_name = pageview.PageName;
    if (pageview.PageIsActive)
    {
      Rhino.RhinoApp.WriteLine("The layout {0} is active", layout_name);
    }
    else
    {
      string detail_name = pageview.ActiveViewport.Name;
      Rhino.RhinoApp.WriteLine("The detail {0} on layout {1} is active", detail_name, layout_name);
    }
  }
  else
  {
    string viewport_name = view.MainViewport.Name;
    Rhino.RhinoApp.WriteLine("The viewport {0} is active", viewport_name);
  }
  return Rhino.Commands.Result.Success;
}
}
