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