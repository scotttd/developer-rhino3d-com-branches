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
public static Rhino.Commands.Result AddLinearDimension(Rhino.RhinoDoc doc)
{
  Rhino.Geometry.LinearDimension dimension;
  Rhino.Commands.Result rc = Rhino.Input.RhinoGet.GetLinearDimension(out dimension);
  if (rc == Rhino.Commands.Result.Success && dimension != null)
  {
    if (doc.Objects.AddLinearDimension(dimension) == Guid.Empty)
      rc = Rhino.Commands.Result.Failure;
    else
      doc.Views.Redraw();
  }
  return rc;
}
}
