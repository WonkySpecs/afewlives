sampler tex : register(s0);
texture lightMask;

sampler lightSampler = sampler_state
{
    Texture = <lightMask>;
};

float4 PixelShaderFunction(float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 col = tex2D(tex, texCoord);
    float4 light = tex2D(lightSampler, texCoord);
    col.rgb *= max(light.rgb, 0.2);
    return col;
}

technique PostProcess
{
    Pass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
