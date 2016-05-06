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
public static Rhino.Commands.Result AddBrepBox(Rhino.RhinoDoc doc)
{
  Rhino.Geometry.Point3d pt0 = new Rhino.Geometry.Point3d(0, 0, 0);
  Rhino.Geometry.Point3d pt1 = new Rhino.Geometry.Point3d(10, 10, 10);
  Rhino.Geometry.BoundingBox box = new Rhino.Geometry.BoundingBox(pt0, pt1);
  Rhino.Geometry.Brep brep = box.ToBrep();
  Rhino.Commands.Result rc = Rhino.Commands.Result.Failure;
  if( doc.Objects.AddBrep(brep) != System.Guid.Empty )
  {
    rc = Rhino.Commands.Result.Success;
    doc.Views.Redraw();
  }
  return rc;
}
}
