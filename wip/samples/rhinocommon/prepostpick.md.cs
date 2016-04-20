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
public class PrePostPickCommand : Command
{
  public override string EnglishName { get { return "csPrePostPick"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var go = new Rhino.Input.Custom.GetObject();
    go.SetCommandPrompt("Select objects");
    go.EnablePreSelect(true, true);
    go.EnablePostSelect(true);
    go.GetMultiple(0, 0);
    if (go.CommandResult() != Result.Success)
      return go.CommandResult();

    var selected_objects = go.Objects().ToList();

    if (go.ObjectsWerePreselected)
    {
      go.EnablePreSelect(false, true);
      go.DeselectAllBeforePostSelect = false;
      go.EnableUnselectObjectsOnExit(false);
      go.GetMultiple(0, 0);
      if (go.CommandResult() == Result.Success)
      {
        foreach (var obj in go.Objects())
        {
          selected_objects.Add(obj);
          // The normal behavior of commands is that when they finish,
          // objects that were pre-selected remain selected and objects
          // that were post-selected will not be selected. Because we
          // potentially could have both, we'll try to do something
          // consistent and make sure post-selected objects also stay selected
          obj.Object().Select(true);
        }
      }
    }
    return selected_objects.Count > 0 ? Result.Success : Result.Nothing;
  }
}
}
