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
public class SetViewNameCommand : Command
{
  public override string EnglishName { get { return "csSetViewName"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var view = doc.Views.ActiveView;
    if (view == null)
      return Result.Failure;
   
    view.MainViewport.Name = "Facade";
    return Result.Success;
  }
}
}