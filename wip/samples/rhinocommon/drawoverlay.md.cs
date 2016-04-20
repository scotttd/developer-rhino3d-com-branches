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
class CustomConduit : Rhino.Display.DisplayConduit
{
  protected override void DrawForeground(Rhino.Display.DrawEventArgs e)
  {
    var bounds = e.Viewport.Bounds;
    var pt = new Rhino.Geometry.Point2d(bounds.Right - 100, bounds.Bottom - 30);
    e.Display.Draw2dText("Hello", System.Drawing.Color.Red, pt, false);
  }
}

[System.Runtime.InteropServices.Guid("58e7d4b7-407a-43b7-867b-3c517dd53d9d")]
public class ex_drawoverlay : Rhino.Commands.Command
{
  public override string EnglishName { get { return "csDrawOverlay"; } }

  readonly CustomConduit m_conduit = new CustomConduit();
  protected override Rhino.Commands.Result RunCommand(RhinoDoc doc, Rhino.Commands.RunMode mode)
  {
    // toggle conduit on/off
    m_conduit.Enabled = !m_conduit.Enabled;
    
    RhinoApp.WriteLine("Custom conduit enabled = {0}", m_conduit.Enabled);
    doc.Views.Redraw();
    return Rhino.Commands.Result.Success;
  }
}
}
