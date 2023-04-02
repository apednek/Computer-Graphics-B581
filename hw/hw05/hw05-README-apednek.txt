B581 / Fall 2022
Homework 05
Aditi Shivaji Pednekar
apednek@iu.edu

I used Unity 2020.3.16f1 version for homework-05.

Task A: Project Proposal

Topic -> An Abstract Scene

3D Scene: Models and Surfaces
- Grass Surface that is bounded.
- Two tall walls attached to the surface so that it forms a small corner of a room.
- These walls would be the "non-trivial" objects. 
Animation -> A giant analog clock with two hands that tick continously shall be placed on one wall.

Interaction: User Interaction and Object-Object Interaction
- Two ball like objects which the user can move. These objects can collide with each other.
- These objects are placed on the grass.
- The user can move these objects by directly interacting with them.
- The position of these two objects are bounded inside the room so that they don't fall out of the grass surface at any point.

Camera:
- Two cameras.
- Both cameras can only move up or down.
- One camera dedicated to show what's in front of the first wall.
- Second camera dedicated to show what's in front of the second wall.
- Both camera views can be switched with HUD controls.

Illumination:
- A lamp post like structure that would be placed on the grass surface.
- This would act as the diffused light and would be the only source of illumination in the scene.
- The light would be switched on/off with the HUD controls.

-----------------------------------------------------------------------------------------------------------------------------------

Task B:
The rotation which I preferred to set for the cubes is x-invariant. This rotation makes all the cubes look distinct.
Moreover, for the second task it gives the best effect.

PS: While going over the requirements of task B I was experimenting with the shader which we had applied for the cube.
I tried scaling the cube with the use of the `mesh vertices`. The commented block of code includes this.
