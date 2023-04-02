B581 / Fall 2022
Lab 01
Aditi Shivaji Pednekar
apednek@iu.edu

- Organizing the GitHub repository for B581
My repository is organized in a way that includes separate folders for homework(hw), lab tasks(labs) and problem tasks(problem-sets).

- Lab Tasks

1. Shadertoy account
Username: apednek
Email: apednek@iu.edu

2. Lab Task Code
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    // Normalized pixel coordinates (from 0 to 1)
    vec2 uv = fragCoord/iResolution.xy;
    
    //Setting the entire graphics context to black
    vec3 col1 = vec3(0.0, 0.0, 0.0);
    
    //Setting the entire graphics context to white
    vec3 col2 = vec3(1.0, 1.0, 1.0);
    
    //Setting the entire graphics context to three other colors
    vec3 col3 = vec3(1.0, 1.0, uv.x);
    
    // Output to screen for col3
    fragColor = vec4(col3,1.0);
}

- For setting the entire graphics context to black the (r,g,b) values of the vector should be set to 0.
- For setting the entire graphics context to white the (r,g,b) values of the vector should be set to 1.
- For setting the entire graphics context to at least three other colors I set the blue parameter of the (r,g,b) to uv.x so that each time mainImage() is called the canvas would show up a horizontal gradient yellow effect starting from (1.0, 1.0, 0.0) and ending at (1.0, 1.0, 1.0).


