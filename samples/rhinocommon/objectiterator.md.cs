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
public class ObjectEnumeratorCommand : Command
{
  public override string EnglishName
  {
    get { return "csObjectEnumerator"; }
  }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var object_enumerator_settings = new ObjectEnumeratorSettings();
    object_enumerator_settings.IncludeLights = true;
    object_enumerator_settings.IncludeGrips = false;
    var rhino_objects = doc.Objects.GetObjectList(object_enumerator_settings);

    int count = 0;
    foreach (var rhino_object in rhino_objects)
    {
      if (rhino_object.IsSelectable() && rhino_object.IsSelected(false) == 0)
      {
        rhino_object.Select(true);
        count++;
      }
    }
    if (count > 0)
    {
      doc.Views.Redraw();
      RhinoApp.WriteLine("{0} object{1} selected", count,
        count == 1 ? "" : "s");
    }
    return Result.Success;
  }
}
}
