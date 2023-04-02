B581 / Fall 2022
Problem Set 04
Aditi Shivaji Pednekar
apednek@iu.edu

I used Unity 2020.3.16f1 version for problem-set-04.

I have completed Task B and three interactions(translating in xy, translating in xz and rotating).

In order to test the working of Task A and Task B:
-> Open scene `Assets/Scenes/SingleSegmentGPUSpline.unity` and play.

I've been doing a lot of trial and error in order to make Task C work. Hence there are multiple extra scripts inside the project.
But the scripts related to Task A and Task B are:
- Cube.cs
- DrawAxis.cs
- CubeShader.shader

The buttons `Rot`, `T-xy`, `T-xz` only have the interactions in place.

In the code I have tried creating the translation matrices for `xy` and `xz` plane and also the rotation matrix.
From this I calculated the `modeling matrix`.
However, I was not able to use this `modeling matrix` inside the shader. So to make the interactions work I did the basic `transform.Translate()` and `transform.Rotate()`.



