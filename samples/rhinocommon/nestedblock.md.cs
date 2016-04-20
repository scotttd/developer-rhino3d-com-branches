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
public static Rhino.Commands.Result AddNestedBlock(RhinoDoc doc)
{
  var circle = new Circle(Point3d.Origin, 5);
  Curve[] curveList = { new ArcCurve(circle) };
  var circleIndex = doc.InstanceDefinitions.Add("Circle", "Circle with radius of 5", Point3d.Origin, curveList);
  var transform = Transform.Identity;
  var irefId = doc.InstanceDefinitions[circleIndex].Id;
  var iref = new InstanceReferenceGeometry(irefId, transform);
  circle.Radius = circle.Radius * 2.0;
  GeometryBase[] blockList = { iref, new ArcCurve(circle) };
  var circle2Index = doc.InstanceDefinitions.Add("TwoCircles", "Nested block test", Point3d.Origin, blockList);
  doc.Objects.AddInstanceObject(circle2Index, transform);
  doc.Views.Redraw();
  return Rhino.Commands.Result.Success;
}
}
