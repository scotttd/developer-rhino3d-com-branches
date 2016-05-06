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
public static Rhino.Commands.Result InstanceDefinitionObjects(Rhino.RhinoDoc doc)
{
  Rhino.DocObjects.ObjRef objref;
  var rc = Rhino.Input.RhinoGet.GetOneObject("Select instance", false, Rhino.DocObjects.ObjectType.InstanceReference, out objref);
  if (rc != Rhino.Commands.Result.Success)
    return rc;

  var iref = objref.Object() as Rhino.DocObjects.InstanceObject;
  if (iref != null)
  {
    var idef = iref.InstanceDefinition;
    if (idef != null)
    {
      var rhino_objects = idef.GetObjects();
      for (int i = 0; i < rhino_objects.Length; i++)
        Rhino.RhinoApp.WriteLine("Object {0} = {1}", i, rhino_objects[i].Id);
    }
  }
  return Rhino.Commands.Result.Success;
}
}
