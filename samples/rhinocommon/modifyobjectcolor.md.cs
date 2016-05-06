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
public class ModifyObjectColorCommand : Command
{
  public override string EnglishName { get { return "csModifyObjectColor"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    ObjRef obj_ref;
    var rc = RhinoGet.GetOneObject("Select object", false, ObjectType.AnyObject, out obj_ref);
    if (rc != Result.Success)
      return rc;
    var rhino_object = obj_ref.Object();
    var color = rhino_object.Attributes.ObjectColor;
    bool b = Rhino.UI.Dialogs.ShowColorDialog(ref color);
    if (!b) return Result.Cancel;

    rhino_object.Attributes.ObjectColor = color;
    rhino_object.Attributes.ColorSource = ObjectColorSource.ColorFromObject;
    rhino_object.CommitChanges();

    // an object's color attributes can also be specified
    // when the object is added to Rhino
    var sphere = new Sphere(Point3d.Origin, 5.0);
    var attributes = new ObjectAttributes();
    attributes.ObjectColor = Color.CadetBlue;
    attributes.ColorSource = ObjectColorSource.ColorFromObject;
    doc.Objects.AddSphere(sphere, attributes);

    doc.Views.Redraw();
    return Result.Success;
  }
}
}
