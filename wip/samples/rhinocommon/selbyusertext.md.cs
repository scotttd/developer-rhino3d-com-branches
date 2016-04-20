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
public class ex_selbyusertext : Rhino.Commands.SelCommand
{
  public override string EnglishName{ get { return "SelByUserText"; } }

  string m_key = string.Empty;
  protected override Rhino.Commands.Result RunCommand(RhinoDoc doc, Rhino.Commands.RunMode mode)
  {
    // You don't have to override RunCommand if you don't need any user input. In
    // this case we want to get a key from the user. If you return something other
    // than Success, the selection is canceled
    return Rhino.Input.RhinoGet.GetString("key", true, ref m_key);
  }

  protected override bool SelFilter(Rhino.DocObjects.RhinoObject rhObj)
  {
    string s = rhObj.Attributes.GetUserString(m_key);
    return !string.IsNullOrEmpty(s);
  }
}
}
