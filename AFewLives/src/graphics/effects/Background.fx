sampler tex : register(s0);
texture lightMask;

sampler lightSampler = sampler_state
{
    Texture = <lightMask>;
};

float4 PixelShaderFunction(float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 col = tex2D(tex, texCoord);
    return lerp(col, tex2D(lightSampler, texCoord), 0.3);
}

technique PostProcess
{
    Pass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
