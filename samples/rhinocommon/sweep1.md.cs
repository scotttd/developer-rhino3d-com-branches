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
public static Rhino.Commands.Result Sweep1(Rhino.RhinoDoc doc)
{
  Rhino.DocObjects.ObjRef rail_ref;
  var rc = RhinoGet.GetOneObject("Select rail curve", false, Rhino.DocObjects.ObjectType.Curve, out rail_ref);
  if(rc!=Rhino.Commands.Result.Success)
    return rc;

  var rail_crv = rail_ref.Curve();
  if( rail_crv==null )
    return Rhino.Commands.Result.Failure;

  var gx = new Rhino.Input.Custom.GetObject();
  gx.SetCommandPrompt("Select cross section curves");
  gx.GeometryFilter = Rhino.DocObjects.ObjectType.Curve;
  gx.EnablePreSelect(false, true);
  gx.GetMultiple(1,0);
  if( gx.CommandResult() != Rhino.Commands.Result.Success )
    return gx.CommandResult();
  
  var cross_sections = new List<Rhino.Geometry.Curve>();
  for( int i=0; i<gx.ObjectCount; i++ )
  {
    var crv = gx.Object(i).Curve();
    if( crv!= null)
      cross_sections.Add(crv);
  }
  if( cross_sections.Count<1 )
    return Rhino.Commands.Result.Failure;

  var sweep = new Rhino.Geometry.SweepOneRail();
  sweep.AngleToleranceRadians = doc.ModelAngleToleranceRadians;
  sweep.ClosedSweep = false;
  sweep.SweepTolerance = doc.ModelAbsoluteTolerance;
  sweep.SetToRoadlikeTop();
  var breps = sweep.PerformSweep(rail_crv, cross_sections);
  for( int i=0; i<breps.Length; i++ )
    doc.Objects.AddBrep(breps[i]);
  doc.Views.Redraw();
  return Rhino.Commands.Result.Success;
}
}
