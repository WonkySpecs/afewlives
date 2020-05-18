texture ScreenTex;
float AlphaFade;
float ColorDrain;

sampler TexSampler = sampler_state
{
    Texture = <ScreenTex>;
};

float4 PixelShaderFunction(float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 col = tex2D(TexSampler, texCoord);
    col.a *= AlphaFade;
    float val = (col.r + col.g + col.b)  / 3;
    return lerp(col, float4(val, val, val, col.a), ColorDrain);
}

technique PostProcess
{
    Pass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
