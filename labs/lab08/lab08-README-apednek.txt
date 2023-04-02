B581 / Fall 2022
Lab 08
Aditi Shivaji Pednekar
apednek@iu.edu

1. What kind of data structure is used by Unity to pass information to a vertex shader about the individual vertex to be processed by the vertex shader?
What is the name of that data structure, what information does that data represent, and what data type(s) does it use?

Data Structure -> struct
Information passed -> vertex position, texture coordinates
Data Types -> Float for both

2. When both a vertex shader and a fragment shader are used in Unity, how does information get passed from the vertex shader's output, as input to the fragment shader: is there a data structure involved?
What is the name of that data structure, what information does that data represent, and what data type(s) may it use?

The output from vertex shader gets interpolated across the face of the rendered triangles and the values at each pixel gets passed to the fragment shader.
Data Structure -> struct
Information passed -> Texture coordinates
Data Types -> Float