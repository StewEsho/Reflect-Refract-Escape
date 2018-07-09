Shader "Custom/VertexShader" {
    Subshader {
        BindChannels {
            Bind "vertex", vertex
            Bind "color", color 
        }
        Pass {}
    }
}

