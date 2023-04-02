B581 / Fall 2022
Problem Set 03
Aditi Shivaji Pednekar
apednek@iu.edu

I used Unity 2020.3.16f1 version for problem-set-03.

I have completed Task A and Task B of problem-set-03.

In order to test the working of Task A and Task B:
-> Open scene `Assets/Scenes/SingleSegmentGPUSpline.unity` and play.

I've been doing a lot of trial and error in order to make Task C work. Hence there are multiple extra scripts inside the project.
But the scripts related to Task A and Task B are:
- DragObject.cs
- SplineSegmentGPUCompute.cs
- SplineParameters.cs
- SplineShader.shader

The `Show Derivative` button allows to turn on/off the display of first and second derivatives. 
Whenever any particular control point is dragged that point is highlighted.

I was able to complete the tasks with the help of the readings and lecture notes.


-----------------------------------------------------------------------------------------------------------------------------

UPDATE - TASK C (OCT 31ST, 2022)

To test Task C:
-> Open scene Assets/Scenes/SingleSegmentGPUSplineTaskC 2.unity

The code for Task C can be found in the following scripts:
- Controller.cs
- TaskC2.cs
- DragObject.cs
- SplineParameters.cs
- SplineShader.shader
 
The script `TaskC2.cs` is basically `SplineSegmentGPUCompute.cs` but little modified to fit the new requirements.

