---
title: Setting a Viewport's Title
description:
author:
apis: ['RhinoCommon']
languages: ['C#', 'Python', 'VB.NET']
platforms: ['Cross-Platform']
categories: ['Viewports and Views']
origin: http://wiki.mcneel.com/developer/rhinocommonsamples/setviewname
order: 1
keywords: ['setting', 'viewports', 'title']
layout: code-sample-rhinocommon
---

```cs
partial class Examples
{
  public static Result SetViewName(RhinoDoc doc)
  {
    var view = doc.Views.ActiveView;
    if (view == null)
      return Result.Failure;

    view.MainViewport.Name = "Facade";
    return Result.Success;
  }
}
```
{: #cs .tab-pane .fade .in .active}


```vbnet
Partial Friend Class Examples
  Public Shared Function SetViewName(ByVal doc As RhinoDoc) As Result
	Dim view = doc.Views.ActiveView
	If view Is Nothing Then
	  Return Result.Failure
	End If

	view.MainViewport.Name = "Facade"
	Return Result.Success
  End Function
End Class
```
{: #vb .tab-pane .fade .in}


```python
from Rhino.Commands import *
import rhinoscriptsyntax as rs
from scriptcontext import doc

def RunCommand():
  view = doc.Views.ActiveView
  if view == None:
    return Result.Failure

  view.MainViewport.Name = "Facade"
  return Result.Success

if __name__ == "__main__":
  RunCommand()
```
{: #py .tab-pane .fade .in}
