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
public class MaximizeViewCommand : Command
{
  [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
  public static extern IntPtr GetParent(IntPtr hWnd);
 
  [DllImport("user32.dll")]
  static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

  public override string EnglishName { get { return "csMaximizeView"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    IntPtr parent_handle = GetParent(doc.Views.ActiveView.Handle);
    if (parent_handle != IntPtr.Zero)
      ShowWindow(parent_handle, 3);
    return Result.Success;
  }
}
}
