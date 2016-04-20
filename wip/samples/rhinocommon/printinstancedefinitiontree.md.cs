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
public class InstanceDefinitionTreeCommand : Command
{
  public override string EnglishName { get { return "csInstanceDefinitionTree"; } }

  protected override Result RunCommand(RhinoDoc doc, RunMode mode)
  {
    var instance_definitions = doc.InstanceDefinitions;
    var instance_definition_count = instance_definitions.Count;

    if (instance_definition_count == 0)
    {
      RhinoApp.WriteLine("Document contains no instance definitions.");
      return Result.Nothing;
    }

    var dump = new TextLog();
    dump.IndentSize = 4;

    for (int i = 0; i < instance_definition_count; i++)
      DumpInstanceDefinition(instance_definitions[i], ref dump, true);

    RhinoApp.WriteLine(dump.ToString());

    return Result.Success;
  }

  private void DumpInstanceDefinition(InstanceDefinition instanceDefinition, ref TextLog dump, bool isRoot)
  {
    if (instanceDefinition != null && !instanceDefinition.IsDeleted)
    {
      string node = isRoot ? "─" : "└";
      dump.Print(string.Format("{0} Instance definition {1} = {2}\n", node, instanceDefinition.Index, instanceDefinition.Name));

      if (instanceDefinition.ObjectCount  > 0)
      {
        dump.PushIndent();
        for (int i = 0; i < instanceDefinition.ObjectCount ; i++)
        {
          var obj = instanceDefinition.Object(i);
          if (obj == null) continue;
          if (obj is InstanceObject)
            DumpInstanceDefinition((obj as InstanceObject).InstanceDefinition, ref dump, false); // Recursive...
          else
            dump.Print("\u2514 Object {0} = {1}\n", i, obj.ShortDescription(false));
        }
        dump.PopIndent();
      }
    }
  }
}
}
