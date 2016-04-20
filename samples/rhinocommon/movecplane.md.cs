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
readonly Rhino.DocObjects.ConstructionPlane m_cplane;
public MoveCPlanePoint(Rhino.DocObjects.ConstructionPlane cplane)
{
  m_cplane = cplane;
}

protected override void OnMouseMove(Rhino.Input.Custom.GetPointMouseEventArgs e)
{
  Plane pl = m_cplane.Plane;
  pl.Origin = e.Point;
  m_cplane.Plane = pl;
}

protected override void OnDynamicDraw(Rhino.Input.Custom.GetPointDrawEventArgs e)
{
  e.Display.DrawConstructionPlane(m_cplane);
}
}
