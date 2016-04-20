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
public static Rhino.Commands.Result IsocurveDensity(Rhino.RhinoDoc doc)
{
  Rhino.DocObjects.ObjRef objref;
  var rc = Rhino.Input.RhinoGet.GetOneObject("Select surface", false, Rhino.DocObjects.ObjectType.Surface, out objref);
  if( rc!= Rhino.Commands.Result.Success )
    return rc;

  var brep_obj = objref.Object() as Rhino.DocObjects.BrepObject;
  if( brep_obj!=null )
  {
    brep_obj.Attributes.WireDensity = 3;
    brep_obj.CommitChanges();
    doc.Views.Redraw();
  }
  return Rhino.Commands.Result.Success;
}
}
