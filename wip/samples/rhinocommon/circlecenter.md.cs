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
public static Rhino.Commands.Result CircleCenter(Rhino.RhinoDoc doc)
{
  Rhino.Input.Custom.GetObject go = new Rhino.Input.Custom.GetObject();
  go.SetCommandPrompt("Select objects");
  go.GeometryFilter = Rhino.DocObjects.ObjectType.Curve;
  go.GeometryAttributeFilter = Rhino.Input.Custom.GeometryAttributeFilter.ClosedCurve;
  go.GetMultiple(1, 0);
  if( go.CommandResult() != Rhino.Commands.Result.Success )
    return go.CommandResult();

  Rhino.DocObjects.ObjRef[] objrefs = go.Objects();
  if( objrefs==null )
    return Rhino.Commands.Result.Nothing;

  double tolerance = doc.ModelAbsoluteTolerance;
  for( int i=0; i<objrefs.Length; i++ )
  {
    // get the curve geometry
    Rhino.Geometry.Curve curve = objrefs[i].Curve();
    if( curve==null )
      continue;
    Rhino.Geometry.Circle circle;
    if( curve.TryGetCircle(out circle, tolerance) )
    {
      Rhino.RhinoApp.WriteLine("Circle{0}: center = {1}", i+1, circle.Center);
    }
  }
  return Rhino.Commands.Result.Success;
}
}
