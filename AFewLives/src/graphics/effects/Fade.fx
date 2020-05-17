texture ScreenTex;
float AlphaFade;

sampler TexSampler = sampler_state
{
    Texture = <ScreenTex>;
};

float4 PixelShaderFunction(float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 col = tex2D(TexSampler, texCoord);
    col.a *= AlphaFade;
    return col;
}

technique Fade
{
    Pass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
