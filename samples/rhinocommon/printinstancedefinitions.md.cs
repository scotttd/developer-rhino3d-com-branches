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
public class InstanceDefinitionNamesCommand : Command
{
  public override string EnglishName { get { return "csInstanceDefinitionNames"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var instance_definition_names = (from instance_definition in doc.InstanceDefinitions 
                                     where instance_definition != null && !instance_definition.IsDeleted
                                     select instance_definition.Name);

    foreach (var n in instance_definition_names)
      RhinoApp.WriteLine("Instance definition = {0}", n);

    return Result.Success;
  }
}
}
